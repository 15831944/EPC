using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using System.Data;
using Formula.Helper;
using System.Configuration;

namespace Interface.Logic
{
    public class DataSynQueueCreator
    {
        public void CreateBaseInfoQueue()
        {
            new BaseEnumFO().CreateBaseMajorPhaseQueue();//阶段、专业枚举
            new BaseEnumFO().CreateBaseMistakeTypeQueue();//质量问题分类枚举
            new AuditAdviceCatalogFO().CreateCatalogQueue();//校审意见库分类
            new UserFO().CreateUserQueue();//用户
            new ProjectFO().CreateProjectQueue();//项目
        }

        public void CreateProjectInfoQueue()
        {
            new AuditAdviceFO().CreateAdviceQueue();//校审意见库，需要先等分类同步完成才能同步意见，所以放在这里调用
            new TaskWorkFO().CreateTaskWorkQueue();//工作包
            new MilestoneFO().CreateMileStoneQueue();//计划（发图、提资）同步给四方
            new DesignInputFO().CreateDesignInputQueue();//设计输入文件夹
            new DesignInputFileFO().CreateDesignInputFileQueue();//设计输入资料

            new GetDataFO().CreateGetQueue();//创建所有数据请求队列}
        }
    }
}
