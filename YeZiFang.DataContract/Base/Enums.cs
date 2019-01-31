﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YeZiFang.DataContract.Base
{
    public enum TableTemplateType
    {
        [Description("用户信息表单")]
        UserForm = 1,
        [Description("文章信息表单")]
        Article = 2
    }

    public enum TableFieldType
    {
        [Description("文本")]
        Text = 1,
        [Description("文本段")]
        TextArea = 2,
        [Description("选择下拉框")]
        Select = 3,
        [Description("单选框")]
        Radio = 4,
        [Description("复选框")]
        CheckBox = 5
    }

    public enum GoodsPlatformType
    {
        [Description("淘宝")]
        TB = 1,
        [Description("天猫")]
        TM = 2,
        [Description("京东")]
        JD = 3
    }
}
