﻿namespace SuperTerminal.Const
{
    public class Rules
    {
        /// <summary>
        /// IPv4
        /// </summary>
        public const string IPv4 = @"(?:(?:(25[0-5])|(2[0-4]\d)|((1\d{2})|([1-9]?\d)))\.){3}(?:(25[0-5])|(2[0-4]\d)|((1\d{2})|([1-9]?\d)))";
        /// <summary>
        /// IPv6
        /// </summary>
        public const string IPv6 = @"^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$";
        /// <summary>
        /// Email验证规则
        /// </summary>
        public const string Email = @"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?";
        /// <summary>
        /// 手机号码验证规则
        /// </summary>
        public const string Phone = @"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$";
        /// <summary>
        /// 字符长度大于5
        /// </summary>
        public const string StringThanLength5 = @"^\s*\S{1}[\S\s]{4,}$";

        /// <summary>
        /// 字符长度最大20
        /// </summary>
        public const string StringLength20 = @"^\s*\S{1}[\S\s]{0,19}$";

        /// <summary>
        /// 字符长度最大50
        /// </summary>
        public const string StringLength50 = @"^\s*\S{1}[\S\s]{0,49}$";

        /// <summary>
        /// 字符长度最大100
        /// </summary>
        public const string StringLength100 = @"^\s*\S{1}[\S\s]{0,99}$";

        /// <summary>
        /// 字符长度最大200
        /// </summary>
        public const string StringLength200 = @"^\s*\S{1}[\S\s]{0,199}$";
        /// <summary>
        /// 必填
        /// </summary>
        public const string Requird = @"^\s*\S+[\S\s]*$";
        /// <summary>
        /// 非空int
        /// </summary>
        public const string RequirdGuid = "^((?!000000)[0-9a-fA-F]){8}(-[0-9a-fA-F]{4}){3}-[0-9a-fA-F]{12}$";
        /// <summary>
        /// 数字
        /// </summary>
        public const string Number = @"^\d+$";
        /// <summary>
        /// 匹配空
        /// </summary>
        public const string Empty = @"^$";
        /// <summary>
        /// decimal (18/4)
        /// </summary>
        public const string Decimal = @"^[1-9]\d{0,13}(\.\d{0,4})?$";

        /// <summary>
        /// 英文
        /// </summary>
        public const string Word = @"^[a-zA-Z]+$";
        /// <summary>
        /// 日期
        /// </summary>
        public const string DateTime = @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))";
    }
}
