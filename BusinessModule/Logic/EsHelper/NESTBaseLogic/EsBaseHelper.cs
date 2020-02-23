using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Elasticsearch.Net;
using Nest;

namespace EsHelper
{
    public class EsBaseHelper
    {
        #region 常量
        //Es节点地址：http://10.10.1.96:9200
        private string url = "";
        public string Url
        {
            get
            {
                return url;
            }
        }

        public string DefaultIndex { get; set; } //默认索引 （默认表）

        private ElasticClient client;
        public ElasticClient Client
        {
            get
            {
                if (client == null)
                {
                    var connectionPool = new SingleNodeConnectionPool(new Uri(this.Url));
                    var settings = new ConnectionSettings(connectionPool);
                    //if (!string.IsNullOrEmpty(this.DefaultIndex))
                    //    settings.DefaultIndex(DefaultIndex);
                    client = new ElasticClient(settings);
                    //if (!string.IsNullOrEmpty(this.DefaultIndex))
                    //    this.CheckIndex(this.DefaultIndex);
                }
                return client;
            }
        }

        #endregion

        #region 构造函数
        public EsBaseHelper(string url)
        {
            this.url = url;
        }
        #endregion

        #region 静态构造方法

        public static EsBaseHelper CreateEsHelper(string url)
        {
            EsBaseHelper esHelper = null;
            esHelper = new EsBaseHelper(url);
            return esHelper;
        }

        #endregion

        #region Index方法

        public bool ExistsIndex(string indexName)
        {
            return Client.Indices.Exists(indexName).IsValid;
        }

        public void CheckIndex(string indexName)
        {
            if (!ExistsIndex(indexName))
                throw new Exception("【ES错误】CheckIndex：" + indexName + "不存在");
        }

        public void CreateIndex<T>(string indexName) where T : class,new()
        {
            if (string.IsNullOrEmpty(indexName))
                throw new Exception("CreateIndex：参数indexName不能为空");
            var re = Client.Indices.Create(indexName + "_" + DateTime.Now.ToString("yyyyMMdd"), c => c
                .Map<T>(m => m.AutoMap())
                .Aliases(a => a.Alias(indexName))
                //.Settings(s=>s.NumberOfShards(1))//设置分片数
                );
            if (!re.IsValid)
            {
                var error = re.OriginalException == null ? re.ServerError.Error.ToString() : re.OriginalException.Message.ToString();
                throw new Exception("【ES错误】CreateIndex：" + error);
            }
        }

        public void DeleteIndex(string indexName)
        {
            //Aliases cannot be used to delete an index
            if (string.IsNullOrEmpty(indexName))
                throw new Exception("DeleteIndex：参数indexName不能为空");
            var re = Client.Indices.Delete(indexName);
            if (!re.IsValid)
            {
                var error = re.OriginalException == null ? re.ServerError.Error.ToString() : re.OriginalException.Message.ToString();
                throw new Exception("【ES错误】DeleteIndex：" + error);
            }
        }

        public void UpdateByQuery()
        {
            var re = Client.UpdateByQuery<EsFile>(a => a.Index(DefaultIndex).Conflicts(Conflicts.Proceed));
            if (!re.IsValid)
            {
                var error = re.OriginalException == null ? re.ServerError.Error.ToString() : re.OriginalException.Message.ToString();
                throw new Exception("【ES错误】UpdateByQuery：" + error);
            }
        }

        public void GetIndexList()
        {

        }

        #endregion

        #region Document方法

        public void AddDocument<T>(T obj) where T : class,new()
        {
            if (string.IsNullOrEmpty(this.DefaultIndex))
                throw new Exception("AddDocument:默认索引(DefaultIndex)不能为空");
            AddDocument(obj, this.DefaultIndex);
        }

        public void AddDocument<T>(T obj, string indexName) where T : class,new()
        {
            var re = Client.Index(obj, i => i.Index(indexName));
            if (!re.IsValid)
            {
                var error = re.OriginalException == null ? re.ServerError.Error.ToString() : re.OriginalException.Message.ToString();
                throw new Exception("【ES错误】AddDocument：" + error);
            }
        }

        public void DeleteAllDocument<T>() where T : class,new()
        {
            if (string.IsNullOrEmpty(this.DefaultIndex))
                throw new Exception("DeleteAllDocument:默认索引(DefaultIndex)不能为空");
            DeleteAllDocument<T>(this.DefaultIndex);
        }

        public void DeleteAllDocument<T>(string indexName) where T : class,new()
        {
            var re = Client.DeleteByQuery<T>(a => a.Index(indexName).Query(q => q.MatchAll()));
            if (!re.IsValid)
            {
                var error = re.OriginalException == null ? re.ServerError.Error.ToString() : re.OriginalException.Message.ToString();
                throw new Exception("【ES错误】DeleteAllDocument：" + error);
            }
        }

        public IHitsMetadata<EsFile> QueryDocument(string queryValue, string spaceID, int pageIndex, int pageSize)
        {
            if (string.IsNullOrEmpty(this.DefaultIndex))
                throw new Exception("QueryAllDocument:默认索引(DefaultIndex)不能为空");
            return QueryDocument(this.DefaultIndex, queryValue, spaceID, pageIndex, pageSize);
        }

        public IHitsMetadata<EsFile> QueryDocument(string indexName, string queryValue, string spaceID, int pageIndex, int pageSize)
        {
            if (string.IsNullOrEmpty(queryValue))
                return new HitsMetadata<EsFile>();
            var re = new SearchResponse<EsFile>();

            Func<QueryContainerDescriptor<EsFile>, QueryContainer> query = null;
            //should 条件 (||)
            var shouldQuerys = new List<Func<QueryContainerDescriptor<EsFile>, QueryContainer>>();
            shouldQuerys.Add(q => q.Match(m => m.Field(f => f.Content).Query(queryValue)));//Content
            shouldQuerys.Add(q => q.Match(m => m.Field(f => f.Title).Query(queryValue)));//Title
            shouldQuerys.Add(q => q.Match(m => m.Field(f => f.PropertyJson).Query(queryValue)));//PropertyJson
            //must 条件 (&&)
            //var mustQuerys = new List<Func<QueryContainerDescriptor<EsFile>, QueryContainer>>();
            if (!string.IsNullOrEmpty(spaceID))//SpaceID
            {
                //mustQuerys.Add(q => q.Term(m => m.Field(f => f.SpaceID).Value(spaceID)));
                query = (q) =>
                {
                    return q.Bool(b => b
                         .Must(
                            m => m.Term(mt => mt.Field(f => f.SpaceID).Value(spaceID))
                            ,m => m.Bool(mb => mb.Should(shouldQuerys))
                         ));
                };
            }
            else
                query = (q) =>
                {
                    return q.Bool(b => b.Should(shouldQuerys));
                };
            re = (SearchResponse<EsFile>)Client.Search<EsFile>(s => s.Index(indexName)
                .From(pageIndex * pageSize)
                .Size(pageSize)
                .Query(query)
                //.Query(q => q.Bool(b => b.Must(mustQuerys).Should(shouldQuerys)))
                .Highlight(h => h
                   .PreTags("<HighlightTag>")
                   .PostTags("</HighlightTag>")
                   .Fields(
                       fs => fs
                           .FragmentSize(50)
                           .NumberOfFragments(3)//显示高亮的条数
                           .Field(f => f.Content),
                       fs => fs.Field(f => f.PropertyJson),
                       fs => fs.Field(f => f.Title)
                   )
               )
                //.Source(false)
               .Source(sf => sf
                    .Excludes(e => e
                        .Fields("content")
                    )
                )
               );
            if (!re.IsValid)
            {
                var error = re.OriginalException == null ? re.ServerError.Error.ToString() : re.OriginalException.Message.ToString();
                throw new Exception("【ES错误】QueryAllDocument：" + error);
            }
            return re.HitsMetadata;
        }

        #endregion

        #region 扩展


        #endregion
    }
}
