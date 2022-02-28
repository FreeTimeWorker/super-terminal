using SuperTerminal.Const;
using SuperTerminal.Enum;
using SuperTerminal.FeildCheck;
namespace SuperTerminal.Model
{
    public class ViewTestModel
    {
        //约定的Id字段,必须有
        public int Id { get; set; }
        [CheckByRange("范围值必须在5-10之间",5,10)]
        public int Range { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [CheckByRegular("电话号码必填", Rules.Requird)]
        [CheckByRegular("电话号码格式错误",Rules.Phone)]
        public string Tel { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [CheckUnique("名称必须唯一","TestModel","Name")]
        public string Name { get; set; }
    }
}
