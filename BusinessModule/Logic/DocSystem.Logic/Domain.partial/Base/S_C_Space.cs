using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Config;
using Formula;

namespace DocSystem.Logic.Domain
{
    public partial class S_DOC_Space
    {
        const string conStrTemplate = "Data Source={0};User ID={1};PWD={2};Initial Catalog={3};Persist Security Info=False;Pooling=true;Min Pool Size=50;Max Pool Size=500;MultipleActiveResultSets=true";

        string _connectString = string.Empty;

        public string ConnectString
        {
            get
            {
                if (String.IsNullOrEmpty(_connectString))
                {
                    _connectString = String.Format(conStrTemplate, this.Server, this.UserName, this.Pwd, this.DbName);
                }
                return _connectString;
            }
        }

        private static S_DOC_NodeAttrDefault[] _DefaultNodeAttrArray = null;
        public static S_DOC_NodeAttrDefault[] DefaultNodeAttrArray
        {
            get
            {
                if (_DefaultNodeAttrArray == null)
                {
                    _DefaultNodeAttrArray = new S_DOC_NodeAttrDefault[] { 
                        new S_DOC_NodeAttrDefault(){ 
                            AttrName="名称",AttrField="Name",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.TextBox.ToString(),
                            ValidateType=ValidateType.BortherUnique.ToString(),Required = TrueOrFalse.True.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "maxLength:200",IsEdit = true,DBItemNullStr = " NOT NULL"
                        },
                        new S_DOC_NodeAttrDefault(){ 
                            AttrName="编号",AttrField="Code",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.TextBox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "maxLength:200",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){ 
                            AttrName="档号",AttrField="DocumentCode",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.TextBox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "maxLength:200",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){ 
                            AttrName="密级",AttrField="SecretLevel",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.Combobox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),IsEnum=TrueOrFalse.True.ToString(),EnumKey="DocConst.SecretLevel",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){ 
                            AttrName="保管期限",AttrField="KeepYear",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.Combobox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),IsEnum=TrueOrFalse.True.ToString(),EnumKey="DocConst.KeepYear",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){ 
                            AttrName="库房名称",AttrField="StorageRoom",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.ButtonEdit.ToString(),
                            Disabled = TrueOrFalse.True.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "maxLength:200",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){ 
                            AttrName="柜架号",AttrField="RackNumber",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.ButtonEdit.ToString(),SelectorKey = "StorageRoomSelector",ReturnParams="value:ID,text:Name,StorageRoom:StorageRoomID,StorageRoom:StorageRoomName",
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "maxLength:200",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){ 
                            AttrName="电子档",AttrField="IsElectronicBox",DataType = AttrDataType.NVarchar50.ToString(),InputType = ControlType.Combobox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),IsEnum=TrueOrFalse.True.ToString(),EnumKey="System.TrueFalse",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){ 
                            AttrName="实物案卷",AttrField="IsPhysicalBox",DataType = AttrDataType.NVarchar50.ToString(),InputType = ControlType.Combobox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),IsEnum=TrueOrFalse.True.ToString(),EnumKey="System.TrueFalse",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){ 
                            AttrName="份数",AttrField="Quantity",DataType = AttrDataType.Int.ToString(),InputType = ControlType.TextBox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "int",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){ 
                            AttrName="库存数量",AttrField="StorageNum",DataType = AttrDataType.Int.ToString(),InputType = ControlType.TextBox.ToString(),
                            Disabled = TrueOrFalse.True.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "int",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){ 
                            AttrName="归档人",AttrField="ArchivePeople",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.ButtonEdit.ToString(),SelectorKey = "SystemUser",ReturnParams="value:ID,text:Name",
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "maxLength:200",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){ 
                            AttrName="归档部门",AttrField="ArchiveDepartment",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.ButtonEdit.ToString(),SelectorKey = "SystemOrg",ReturnParams="value:ID,text:Name",
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "maxLength:200",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){ 
                            AttrName="归档时间",AttrField="ArchiveDate",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.DatePicker.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "maxLength:200",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){
                            AttrName="卷内电子文件数",AttrField="ElectronicFileCount",DataType = AttrDataType.Int.ToString(),InputType = ControlType.TextBox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "int",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){
                            AttrName="卷内实物文件数",AttrField="PhysicalFileCount",DataType = AttrDataType.Int.ToString(),InputType = ControlType.TextBox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "int",IsEdit = true
                        },
                        new S_DOC_NodeAttrDefault(){AttrName="SpaceID",AttrField="SpaceID",DataType = AttrDataType.Varchar50.ToString(),DBItemNullStr = " NOT NULL"},
                        new S_DOC_NodeAttrDefault(){AttrName="ParentID",AttrField="ParentID",DataType = AttrDataType.Varchar500.ToString()},
                        new S_DOC_NodeAttrDefault(){AttrName="FullPathID",AttrField="FullPathID",DataType = AttrDataType.Varchar500.ToString(),DBItemNullStr = " NOT NULL"},
                        new S_DOC_NodeAttrDefault(){AttrName="FullPathName",AttrField="FullPathName",DataType = AttrDataType.NVarcharMax.ToString()},
                        new S_DOC_NodeAttrDefault(){AttrName="ConfigID",AttrField="ConfigID",DataType = AttrDataType.Varchar50.ToString(),DBItemNullStr = " NOT NULL"},
                        new S_DOC_NodeAttrDefault(){AttrName="RelateID",AttrField="RelateID",DataType = AttrDataType.Varchar500.ToString()},
                        new S_DOC_NodeAttrDefault(){AttrName="State",AttrField="State",DataType = AttrDataType.NVarchar50.ToString(),DBItemNullStr = " NOT NULL"},
                        new S_DOC_NodeAttrDefault(){AttrName="CreateTime",AttrField="CreateTime",DataType = AttrDataType.DateTime.ToString()},
                        new S_DOC_NodeAttrDefault(){AttrName="BorrowState",AttrField="BorrowState",DataType = AttrDataType.NVarchar50.ToString()},
                        new S_DOC_NodeAttrDefault(){AttrName="BorrowUserID",AttrField="BorrowUserID",DataType = AttrDataType.NVarchar50.ToString()},
                        new S_DOC_NodeAttrDefault(){AttrName="BorrowUserName",AttrField="BorrowUserName",DataType = AttrDataType.NVarchar50.ToString()},
                        new S_DOC_NodeAttrDefault(){AttrName="SortIndex",AttrField="SortIndex",DataType = AttrDataType.Decimal.ToString()}
                    };
                }
                return _DefaultNodeAttrArray;
            }
        }

        private static S_DOC_FileAttrDefault[] _DefaultFileAttrArray = null;
        public static S_DOC_FileAttrDefault[] DefaultFileAttrArray
        {
            get
            {
                if (_DefaultFileAttrArray == null)
                {
                    _DefaultFileAttrArray = new S_DOC_FileAttrDefault[] { 
                        new S_DOC_FileAttrDefault(){ 
                            FileAttrName="名称",FileAttrField="Name",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.TextBox.ToString(),
                            ValidateType = ValidateType.BortherUnique.ToString(),Required = TrueOrFalse.True.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "maxLength:200",IsEdit = true,DBItemNullStr = " NOT NULL"
                        },
                        new S_DOC_FileAttrDefault(){ 
                            FileAttrName="图纸编号",FileAttrField="Code",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.TextBox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "maxLength:200",IsEdit = true
                        },
                        new S_DOC_FileAttrDefault(){ 
                            FileAttrName="存储类型",FileAttrField="StorageType",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.Combobox.ToString(),Required = TrueOrFalse.True.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),IsEnum=TrueOrFalse.True.ToString(),EnumKey="DocConst.StorageType",IsEdit = true
                        },
                        new S_DOC_FileAttrDefault(){ 
                            FileAttrName="载体",FileAttrField="Media",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.Combobox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),IsEnum=TrueOrFalse.True.ToString(),EnumKey="DocConst.Media",IsEdit = true
                        },
                        new S_DOC_FileAttrDefault(){ 
                            FileAttrName="份数",FileAttrField="Quantity",DataType = AttrDataType.Int.ToString(),InputType = ControlType.TextBox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "int",IsEdit = true
                        },
                        new S_DOC_FileAttrDefault(){ 
                            FileAttrName="库存数量",FileAttrField="StorageNum",DataType = AttrDataType.Int.ToString(),InputType = ControlType.TextBox.ToString(),
                            Disabled = TrueOrFalse.True.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "int",IsEdit = true
                        },
                        new S_DOC_FileAttrDefault(){ 
                            FileAttrName="密级",FileAttrField="SecretLevel",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.Combobox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),IsEnum=TrueOrFalse.True.ToString(),EnumKey="DocConst.SecretLevel",IsEdit = true
                        },
                        new S_DOC_FileAttrDefault(){ 
                            FileAttrName="保管期限",FileAttrField="KeepYear",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.Combobox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),IsEnum=TrueOrFalse.True.ToString(),EnumKey="DocConst.KeepYear",IsEdit = true
                        },
                        new S_DOC_FileAttrDefault(){ 
                            FileAttrName="版本",FileAttrField="Version",DataType = AttrDataType.Decimal.ToString(),InputType = ControlType.TextBox.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "float",IsEdit = true
                        },
                        new S_DOC_FileAttrDefault(){ 
                            FileAttrName="归档人",FileAttrField="ArchivePeople",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.ButtonEdit.ToString(),SelectorKey = "SystemUser",ReturnParams="value:ID,text:Name",
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "maxLength:200",IsEdit = true
                        },
                        new S_DOC_FileAttrDefault(){ 
                            FileAttrName="归档部门",FileAttrField="ArchiveDepartment",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.ButtonEdit.ToString(),SelectorKey = "SystemOrg",ReturnParams="value:ID,text:Name",
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "maxLength:200",IsEdit = true
                        },
                        new S_DOC_FileAttrDefault(){ 
                            FileAttrName="归档时间",FileAttrField="ArchiveDate",DataType = AttrDataType.NVarchar200.ToString(),InputType = ControlType.DatePicker.ToString(),
                            Disabled = TrueOrFalse.False.ToString(),Visible = TrueOrFalse.True.ToString(),VType = "maxLength:200",IsEdit = true
                        },
                        new S_DOC_FileAttrDefault(){ FileAttrName="DocIndexID",FileAttrField="DocIndexID",DataType = AttrDataType.Int.ToString(),DBItemNullStr = " IDENTITY(1,1) NOT NULL"},
                        new S_DOC_FileAttrDefault(){ FileAttrName="NodeID",FileAttrField="NodeID",DataType = AttrDataType.Varchar50.ToString(),DBItemNullStr = " NOT NULL"},
                        new S_DOC_FileAttrDefault(){ FileAttrName="SpaceID",FileAttrField="SpaceID",DataType = AttrDataType.Varchar50.ToString(),DBItemNullStr = " NOT NULL"},
                        new S_DOC_FileAttrDefault(){ FileAttrName="ConfigID",FileAttrField="ConfigID",DataType = AttrDataType.Varchar50.ToString(),DBItemNullStr = " NOT NULL"},
                        new S_DOC_FileAttrDefault(){ FileAttrName="RelateID",FileAttrField="RelateID",DataType = AttrDataType.Varchar500.ToString()},
                        new S_DOC_FileAttrDefault(){ FileAttrName="FullNodeID",FileAttrField="FullNodeID",DataType = AttrDataType.Varchar500.ToString(),DBItemNullStr = " NOT NULL"},
                        new S_DOC_FileAttrDefault(){ FileAttrName="FullNodeName",FileAttrField="FullNodeName",DataType = AttrDataType.NVarchar500.ToString()},
                        new S_DOC_FileAttrDefault(){ FileAttrName="State",FileAttrField="State",DataType = AttrDataType.NVarchar50.ToString()},
                        new S_DOC_FileAttrDefault(){ FileAttrName="CreateTime",FileAttrField="CreateTime",DataType = AttrDataType.DateTime.ToString()},
                        new S_DOC_FileAttrDefault(){ FileAttrName="BorrowState",FileAttrField="BorrowState",DataType = AttrDataType.NVarchar50.ToString()}
                    };
                }
                return _DefaultFileAttrArray;
            }
        }

        public void DoCopy()
        {
            var context = FormulaHelper.GetEntities<DocConfigEntities>();

            var newSpace = this.Clone<S_DOC_Space>();
            newSpace.ID = FormulaHelper.CreateGuid();
            newSpace.SpaceKey = newSpace.SpaceKey + "_Copy";
            newSpace.Name = newSpace.Name + "_Copy";
            newSpace.DbName = newSpace.DbName + "_Copy";
            newSpace.Remark = "";
            context.Set<S_DOC_Space>().Add(newSpace);

            #region 节点定义

            var nodeList = this.S_DOC_Node.ToList();
            var nodeRelation = new Dictionary<string, string>();
            foreach (var node in nodeList)
            {
                var newNode = node.Clone<S_DOC_Node>();
                newNode.ID = FormulaHelper.CreateGuid();
                newNode.SpaceID = newSpace.ID;
                context.Set<S_DOC_Node>().Add(newNode);
                nodeRelation.Add(node.ID, newNode.ID);
                //节点属性 
                var nodeAttrList = node.S_DOC_NodeAttr.ToList();
                foreach (var nodeAttr in nodeAttrList)
                {
                    var newNodeAttr = nodeAttr.Clone<S_DOC_NodeAttr>();
                    newNodeAttr.ID = FormulaHelper.CreateGuid();
                    newNodeAttr.NodeID = newNode.ID;
                    newNodeAttr.SpaceID = newSpace.ID;
                    context.Set<S_DOC_NodeAttr>().Add(newNodeAttr);
                }
                //列表定义
                var listConfigList = this.S_DOC_ListConfig.Where(a => a.RelationID == node.ID).ToList();
                foreach (var listConfig in listConfigList)
                {
                    var newListConfig = listConfig.Clone<S_DOC_ListConfig>();
                    newListConfig.ID = FormulaHelper.CreateGuid();
                    newListConfig.RelationID = newNode.ID;
                    newListConfig.SpaceID = newSpace.ID;
                    context.Set<S_DOC_ListConfig>().Add(newListConfig);
                    //列表定义
                    foreach (var listConfigDetail in listConfig.S_DOC_ListConfigDetail.ToList())
                    {
                        var newListConfigDetail = listConfigDetail.Clone<S_DOC_ListConfigDetail>();
                        newListConfigDetail.ID = 0;
                        newListConfigDetail.ListConfigID = newListConfig.ID;
                        context.Set<S_DOC_ListConfigDetail>().Add(newListConfigDetail);
                    }
                    //查询定义
                    foreach (var listConfigQuery in listConfig.S_DOC_QueryParam.ToList())
                    {
                        var newListConfigQuery = listConfigQuery.Clone<S_DOC_QueryParam>();
                        newListConfigQuery.ID = 0;
                        newListConfigQuery.ListConfigID = newListConfig.ID;
                        context.Set<S_DOC_QueryParam>().Add(newListConfigQuery);
                    }
                }
            }
            #endregion
            #region 节点结构定义

            var rootStructList = this.S_DOC_NodeStrcut.Where(c => c.NodeID == "Root").ToList();
            foreach (var rootStruct in rootStructList)
            {
                var newRootStruct = rootStruct.Clone<S_DOC_NodeStrcut>();
                newRootStruct.ID = FormulaHelper.CreateGuid();
                newRootStruct.FullPathID = newRootStruct.ID;
                newRootStruct.NodeID = rootStruct.NodeID;
                newRootStruct.SpaceID = newSpace.ID;

                context.Set<S_DOC_NodeStrcut>().Add(newRootStruct);

                if (rootStruct.Children != null)
                    CopyNodeStrcut(rootStruct, newRootStruct, context, nodeRelation);
            }
            #endregion
            #region 文件定义

            var fileRelation = new Dictionary<string, string>();
            foreach (var file in this.S_DOC_File.ToList())
            {
                var newFile = file.Clone<S_DOC_File>();
                newFile.ID = FormulaHelper.CreateGuid();
                newFile.SpaceID = newSpace.ID;
                context.Set<S_DOC_File>().Add(newFile);
                fileRelation.Add(file.ID, newFile.ID);
                //节点属性 
                var fileAttrList = file.S_DOC_FileAttr.ToList();
                foreach (var fileAttr in fileAttrList)
                {
                    var newFileAttr = fileAttr.Clone<S_DOC_FileAttr>();
                    newFileAttr.ID = FormulaHelper.CreateGuid();
                    newFileAttr.FileID = newFile.ID;
                    newFileAttr.SpaceID = newSpace.ID;
                    context.Set<S_DOC_FileAttr>().Add(newFileAttr);
                }
                //列表定义
                var listConfigList = this.S_DOC_ListConfig.Where(a => a.RelationID == file.ID).ToList();
                foreach (var listConfig in listConfigList)
                {
                    var newListConfig = listConfig.Clone<S_DOC_ListConfig>();
                    newListConfig.ID = FormulaHelper.CreateGuid();
                    newListConfig.RelationID = newFile.ID;
                    newListConfig.SpaceID = newSpace.ID;
                    context.Set<S_DOC_ListConfig>().Add(newListConfig);
                    //列表定义
                    foreach (var listConfigDetail in listConfig.S_DOC_ListConfigDetail.ToList())
                    {
                        var newListConfigDetail = listConfigDetail.Clone<S_DOC_ListConfigDetail>();
                        newListConfigDetail.ID = 0;
                        newListConfigDetail.ListConfigID = newListConfig.ID;
                        context.Set<S_DOC_ListConfigDetail>().Add(newListConfigDetail);
                    }
                    //查询定义
                    foreach (var listConfigQuery in listConfig.S_DOC_QueryParam.ToList())
                    {
                        var newListConfigQuery = listConfigQuery.Clone<S_DOC_QueryParam>();
                        newListConfigQuery.ID = 0;
                        newListConfigQuery.ListConfigID = newListConfig.ID;
                        context.Set<S_DOC_QueryParam>().Add(newListConfigQuery);
                    }
                }
                //节点文件关系
                foreach (var fileNodeRelation in file.S_DOC_FileNodeRelation.ToList())
                {
                    var newFileNodeRelation = fileNodeRelation.Clone<S_DOC_FileNodeRelation>();
                    newFileNodeRelation.ID = 0;
                    newFileNodeRelation.FileID = fileRelation[fileNodeRelation.FileID];
                    newFileNodeRelation.NodeID = nodeRelation[fileNodeRelation.NodeID];
                    context.Set<S_DOC_FileNodeRelation>().Add(newFileNodeRelation);
                }

                //相册节点定义
                foreach (var atlas in file.S_DOC_AtlasConfigDetail.ToList())
                {
                    var newAtlas = atlas.Clone<S_DOC_AtlasConfigDetail>();
                    newAtlas.ID = 0;
                    newAtlas.FileID = newFile.ID;
                    context.Set<S_DOC_AtlasConfigDetail>().Add(newAtlas);
                }
            }

            #endregion
            #region 树形定义
            //查询定义
            foreach (var treeConfig in this.S_DOC_TreeConfig.ToList())
            {
                var newTreeConfig = treeConfig.Clone<S_DOC_TreeConfig>();
                newTreeConfig.ID = FormulaHelper.CreateGuid();
                newTreeConfig.SpaceID = newSpace.ID;
                context.Set<S_DOC_TreeConfig>().Add(newTreeConfig);
            }
            #endregion
            #region 整编区定义

            foreach (var reorganizeConfig in this.S_DOC_ReorganizeConfig.ToList())
            {
                var newReorganizeConfig = reorganizeConfig.Clone<S_DOC_ReorganizeConfig>();
                newReorganizeConfig.ID = FormulaHelper.CreateGuid();
                newReorganizeConfig.SpaceID = newSpace.ID;
                context.Set<S_DOC_ReorganizeConfig>().Add(newReorganizeConfig);
            }
            #endregion
        }

        private void CopyNodeStrcut(S_DOC_NodeStrcut nodeStruct, S_DOC_NodeStrcut newStruct, DocConfigEntities context, Dictionary<string, string> nodeRelation)
        {
            var childStructList = nodeStruct.Children;
            foreach (var childStruct in childStructList)
            {
                var newChildStruct = childStruct.Clone<S_DOC_NodeStrcut>();
                newChildStruct.ID = FormulaHelper.CreateGuid();
                newChildStruct.ParentID = newStruct.ID;
                newChildStruct.FullPathID = newStruct.FullPathID + "." + newChildStruct.ID;
                newChildStruct.NodeID = nodeRelation[childStruct.NodeID];
                newChildStruct.SpaceID = newStruct.SpaceID;
                context.Set<S_DOC_NodeStrcut>().Add(newChildStruct);

                if (childStruct.Children != null)
                    CopyNodeStrcut(childStruct, newChildStruct, context, nodeRelation);
            }
        }

        public void SaveDataBase()
        {
            if (!this.IsExistDocSpace())
            {
                string createDataBaseSql = " Create DataBase " + this.DbName;
                var result = this.GetDocConfigSqlHelper().ExecuteNonQuery(createDataBaseSql);
            }
            var instanceDB = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" use " + this.DbName + " ");
            sqlBuilder.Append(NodeConfigDDL());
            instanceDB.ExecuteNonQuery(sqlBuilder.ToString());
            sqlBuilder.Clear(); sqlBuilder.Append(" use " + this.DbName + " ");
            sqlBuilder.Append(FileConfigDDL());
            instanceDB.ExecuteNonQuery(sqlBuilder.ToString());
            sqlBuilder.Clear(); sqlBuilder.Append(" use " + this.DbName + " ");
            sqlBuilder.Append(DDLHelper.GetDefaultDDL());
            instanceDB.ExecuteNonQuery(sqlBuilder.ToString());
        }

        public string NodeConfigDDL()
        {
            string tbName = "S_NodeInfo";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" if not exists (select * from dbo.sysobjects " +
                "where id = object_id(N'[dbo].[" + tbName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) ");
            StringBuilder CreateTbSql = new StringBuilder();
            StringBuilder UpdateTbSql = new StringBuilder();
            CreateTbSql.AppendLine(" CREATE TABLE [dbo].[" + tbName + "] ( ");
            CreateTbSql.AppendLine(" [ID] [varchar](50) NOT NULL,");
            foreach (var item in S_DOC_Space.DefaultNodeAttrArray)
            {
                CreateTbSql.AppendLine(" [" + item.AttrField + "] " + DDLHelper.GetFieldDataTypeDDL(item.DataType) + item.DBItemNullStr + ", ");
            }
            var defaultNodeAttrFields = S_DOC_Space.DefaultNodeAttrArray.Select(a => a.AttrField).ToArray();
            var list = this.ToNodeFieldList();
            foreach (var item in list)
            {
                if (!defaultNodeAttrFields.Contains(item.FieldName))
                    CreateTbSql.AppendLine(" [" + item.FieldName + "] " + DDLHelper.GetFieldDataTypeDDL(item.DataType) + "  NULL, ");
            }
            UpdateTbSql.AppendLine(GetNodeConfigAlterDDL());
            CreateTbSql.AppendLine(@" CONSTRAINT [PK_S_NodeInfo] PRIMARY KEY CLUSTERED  ( [ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] ");

            if (String.IsNullOrEmpty(UpdateTbSql.ToString().Trim()))
            { sql.Append(" begin " + CreateTbSql.ToString() + " end "); }
            else
            {
                sql.Append(" begin " + CreateTbSql.ToString() + " end else ");
                sql.Append(" begin " + UpdateTbSql.ToString() + " end ");
            }
            return sql.ToString();
        }

        public string FileConfigDDL()
        {
            string tbName = "S_FileInfo";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" if not exists (select * from dbo.sysobjects " +
                "where id = object_id(N'[dbo].[" + tbName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) ");
            StringBuilder CreateTbSql = new StringBuilder();
            StringBuilder UpdateTbSql = new StringBuilder();
            CreateTbSql.AppendLine(" CREATE TABLE [dbo].[" + tbName + "] ( ");
            CreateTbSql.AppendLine(" [ID] [varchar](50) NOT NULL,");
            foreach (var item in S_DOC_Space.DefaultFileAttrArray)
            {
                CreateTbSql.AppendLine(" [" + item.FileAttrField + "] " + DDLHelper.GetFieldDataTypeDDL(item.DataType) + item.DBItemNullStr + ", ");
            }
            var defaultFileAttrFields = S_DOC_Space.DefaultFileAttrArray.Select(a => a.FileAttrField).ToArray();
            var list = this.ToFileFieldList();
            foreach (var item in list)
            {
                if (!defaultFileAttrFields.Contains(item.FieldName))
                    CreateTbSql.AppendLine(" [" + item.FieldName + "] " + DDLHelper.GetFieldDataTypeDDL(item.DataType) + "  NULL, ");
            }
            UpdateTbSql.AppendLine(GetFileConfigAlterDDL());
            CreateTbSql.AppendLine(@" CONSTRAINT [PK_S_FileInfo] PRIMARY KEY CLUSTERED  ( [ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] ");

            CreateTbSql.AppendLine(@"
ALTER TABLE [dbo].[S_FileInfo]  WITH CHECK ADD  CONSTRAINT [FK_S_FileInfo_S_NodeInfo] FOREIGN KEY([NodeID])
REFERENCES [dbo].[S_NodeInfo] ([ID])
ALTER TABLE [dbo].[S_FileInfo] CHECK CONSTRAINT [FK_S_FileInfo_S_NodeInfo]");

            if (String.IsNullOrEmpty(UpdateTbSql.ToString().Trim()))
            { sql.Append(" begin " + CreateTbSql.ToString() + " end "); }
            else
            {
                sql.Append(" begin " + CreateTbSql.ToString() + " end else ");
                sql.Append(" begin " + UpdateTbSql.ToString() + " end ");
            }
            return sql.ToString();
        }

        private string GetNodeConfigAlterDDL()
        {
            StringBuilder result = new StringBuilder();
            var defaultFieldList = S_DOC_Space.DefaultNodeAttrArray.Select(a => new Field { FieldName = a.AttrField, DataType = a.DataType }).ToList();
            defaultFieldList.AddRange(S_DOC_Space.DefaultNodeAttrArray.Where(a => a.InputType.IndexOf(ControlType.ButtonEdit.ToString()) >= 0).Select(a => new Field { FieldName = a.AttrField + "Name", DataType = a.DataType }).ToList());
            foreach (var item in defaultFieldList)
                result.AppendLine(DDLHelper.GetNodeFieldDDL(item));
            foreach (var item in this.ToNodeFieldList())
                result.AppendLine(DDLHelper.GetNodeFieldDDL(item));
            return result.ToString();
        }

        private string GetFileConfigAlterDDL()
        {
            StringBuilder result = new StringBuilder();
            var defaultFieldList = S_DOC_Space.DefaultFileAttrArray.Select(a => new Field { FieldName = a.FileAttrField, DataType = a.DataType }).ToList();
            defaultFieldList.AddRange(S_DOC_Space.DefaultFileAttrArray.Where(a => a.InputType.IndexOf(ControlType.ButtonEdit.ToString()) >= 0).Select(a => new Field { FieldName = a.FileAttrField + "Name", DataType = a.DataType }).ToList());
            foreach (var item in defaultFieldList)
                result.AppendLine(DDLHelper.GetFileFieldDDL(item));
            foreach (var item in this.ToFileFieldList())
                result.AppendLine(DDLHelper.GetFileFieldDDL(item));
            return result.ToString();
        }

        private bool IsExistDocSpace()
        {

            //试图调用文档空间数据库
            string sqlString = "use master " +
                "select * from sysdatabases where [name] ='" + this.DbName + "'";
            var db = SQLHelper.CreateSqlHelper(ConnEnum.InfrasBaseConfig);
            DataTable dt = db.ExecuteDataTable(sqlString);
            var result = false;
            //判断当前的数据库名称是否存在
            if (dt != null && dt.Rows.Count > 0)
                result = true;
            return result;
        }

        List<Field> _nodefields;
        public List<Field> ToNodeFieldList()
        {
            if (_nodefields == null)
            {
                _nodefields = new List<Field>();
                var configNodes = this.S_DOC_Node.ToList();
                foreach (var nodeConfig in configNodes)
                {
                    var configAttrList = nodeConfig.S_DOC_NodeAttr.Where(d => d.AttrType == AttrType.Custom.ToString()).OrderBy(d => d.AttrSort).ToList();
                    foreach (var item in configAttrList)
                    {
                        if (_nodefields.Exists(d => d.FieldName == item.AttrField))
                            continue;
                        var field = new Field();
                        field.DataType = item.DataType;
                        field.Required = item.Required;
                        field.FieldName = item.AttrField;
                        _nodefields.Add(field);
                        if (item.InputType.IndexOf(ControlType.ButtonEdit.ToString()) >= 0)
                        {
                            var _field = new Field();
                            _field.DataType = item.DataType;
                            _field.Required = item.Required;
                            _field.FieldName = item.AttrField + "Name";
                            _nodefields.Add(_field);
                        }
                    }
                }
            }
            return _nodefields;
        }

        List<Field> _filefields;
        public List<Field> ToFileFieldList()
        {
            if (_filefields == null)
            {
                _filefields = new List<Field>();
                var fileNodes = this.S_DOC_File.ToList();
                foreach (var nodeConfig in fileNodes)
                {
                    var configAttrList = nodeConfig.S_DOC_FileAttr.Where(d => d.AttrType == AttrType.Custom.ToString()).OrderBy(d => d.AttrSort).ToList();
                    foreach (var item in configAttrList)
                    {
                        if (_filefields.Exists(d => d.FieldName == item.FileAttrField))
                            continue;
                        var field = new Field();
                        field.DataType = item.DataType;
                        field.Required = item.Required;
                        field.FieldName = item.FileAttrField;
                        _filefields.Add(field);
                        if (item.InputType.IndexOf(ControlType.ButtonEdit.ToString()) >= 0)
                        {
                            var _field = new Field();
                            _field.DataType = item.DataType;
                            _field.Required = item.Required;
                            _field.FieldName = item.FileAttrField + "Name";
                            _filefields.Add(_field);
                        }
                    }
                }
            }
            return _filefields;
        }

    }
}
