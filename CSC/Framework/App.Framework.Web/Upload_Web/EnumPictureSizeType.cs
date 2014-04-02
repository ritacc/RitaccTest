using System;

namespace App.Framework.Web.Upload
{
    /// <summary>
    /// 文件显示的尺寸
    /// </summary>
    public enum EnumPictureSizeType
    {
        // 重构时注意：
        // 若需要其它尺寸的图片，可以在后面按照格式进行添加。
        // 如需要宽度（用W表示）200，高度（用H表示）200的图片，则添加一个枚举为 W200H200。

        /// <summary>
        /// 显示默认图片
        /// </summary>
        Default,

        /// <summary>
        /// W60H60
        /// </summary>
        W60H60,

        /// <summary>
        /// W80H80
        /// </summary>
        W80H80,

        /// <summary>
        /// W100H100
        /// </summary>
        W100H100,

        /// <summary>
        /// W120H120
        /// </summary>
        W120H120,

        /// <summary>
        /// W120H60
        /// </summary>
        W120H60,

        /// <summary>
        /// W60H120
        /// </summary>
        W60H120,

        /// <summary>
        /// W110H50
        /// </summary>
        W110H50,

        /// <summary>
        /// W90H40
        /// </summary>
        W90H40,

        /// <summary>
        /// W310H390
        /// </summary>
        W310H390,

        /// <summary>
        /// W220H280
        /// </summary>
        W220H280,

        /// <summary>
        /// W120H150
        /// </summary>
        W120H150,

        /// <summary>
        /// W80H100
        /// </summary>
        W80H100,

        /// <summary>
        /// W50H64
        /// </summary>
        W50H64,

        /// <summary>
        /// W160H200
        /// </summary>
        W160H200,

        /// <summary>
        /// W140H175
        /// </summary>
        W140H175,

        /// <summary>
        /// W88H31
        /// </summary>
        W88H31,

        /// <summary>
        /// W710H225
        /// </summary>
        W710H225,

        /// <summary>
        /// W980H255
        /// </summary>
        W980H255,

        /// <summary>
        /// W210H75
        /// </summary>
        W210H75,

        /// <summary>
        /// W104H136
        /// </summary>
        W104H136,
    }
}