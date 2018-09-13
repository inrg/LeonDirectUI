﻿using LeonDirectUI.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeonDirectUI.DUIControl
{
    /// <summary>
    /// 控件基类
    /// </summary>
    public class ControlBase : IPaintable
    {
        #region 字段

        /// <summary>
        /// 绘制器
        /// </summary>
        public IPaint Painter { get; set; }

        #endregion

        #region 基本属性
        //TODO: 可见性和可用性影响显示效果和交互效果
        //TODO: 属性变化触发响应的变化事件

        /// <summary>
        /// 控件的名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 可用性
        /// </summary>
        public virtual bool Enabled { get; set; }

        /// <summary>
        /// 可见性
        /// </summary>
        public virtual bool Visible { get; set; }

        #endregion

        #region 显示属性
        //TODO: 每次显示属性发生变动需要调用绘制方法

        /// <summary>
        /// 显示文本
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// 文本的显示位置
        /// </summary>
        public virtual ContentAlignment TextAlign { get; set; }

        /// <summary>
        /// 字体
        /// </summary>
        public virtual Font Font { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public virtual Color ForeColor { get; set; }

        /// <summary>
        /// 控件显示图像
        /// </summary>
        public virtual Image Image { get; set; }

        /// <summary>
        /// 图像的显示位置
        /// </summary>
        public virtual ContentAlignment ImageAlign { get; set; }

        /// <summary>
        /// 背景图像
        /// </summary>
        public virtual Image BackgroundImage { get; set; }

        /// <summary>
        /// 背景图显示方式
        /// </summary>
        public virtual ImageLayout BackgroundImageLayout { get; set; }

        #endregion

        #region 区域

        /// <summary>
        /// 区域 (核心)
        /// </summary>
        private Rectangle Papa = Rectangle.Empty;

        /// <summary>
        /// 左坐标
        /// </summary>
        public virtual int Left
        {
            get => Papa.X;
            set
            {
                Papa.X = value;
                Painter?.Paint(this);
            }
        }

        /// <summary>
        /// 上坐标
        /// </summary>
        public virtual int Top
        {
            get => Papa.Y;
            set
            {
                Papa.Y = value;
                Painter?.Paint(this);
            }
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public virtual int Width
        {
            get => Papa.Width;
            set
            {
                Papa.Width = value;
                Painter?.Paint(this);
            }
        }

        /// <summary>
        /// 高度
        /// </summary>
        public virtual int Height
        {
            get => Papa.Height;
            set
            {
                Papa.Height = value;
                Painter?.Paint(this);
            }
        }

        /// <summary>
        /// 右边界
        /// </summary>
        public virtual int Right { get => Papa.Right; }

        /// <summary>
        /// 下边界
        /// </summary>
        public virtual int Bottom { get => Papa.Bottom; }

        /// <summary>
        /// 显示区域
        /// </summary>
        public virtual Rectangle Rectangle
        {
            get => Papa;
            set
            {
                Papa = value;
                Painter?.Paint(this);
            }
        }

        /// <summary>
        /// 控件尺寸
        /// </summary>
        public virtual Size Size
        {
            get => Papa.Size;
            set
            {
                Papa.Size = value;
                Painter?.Paint(this);
            }
        }

        /// <summary>
        /// 控件坐标
        /// </summary>
        public virtual Point Location
        {
            get => Papa.Location;
            set
            {
                Papa.Location = value;
                Painter?.Paint(this);
            }
        }

        /// <summary>
        /// 设置边界
        /// </summary>
        /// <param name="left">左坐标</param>
        /// <param name="top">上坐标</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public virtual void SetBounds(int left, int top, int width, int height)
        {
            Papa.X = left;
            Papa.Y = top;
            Papa.Width = width;
            Papa.Height = height;
            
            Painter?.Paint(this);
        }

        /// <summary>
        /// 设置尺寸
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public virtual void SetSize(int width, int height)
        {
            Papa.Width = width;
            Papa.Height = height;
            
            Painter?.Paint(this);
        }

        /// <summary>
        /// 设置坐标
        /// </summary>
        /// <param name="left">左坐标</param>
        /// <param name="top">上坐标</param>
        public virtual void SetLocation(int left, int top)
        {
            Papa.X = left;
            Papa.Y = Height;
            
            Painter?.Paint(this);
        }

        #endregion

        #region 区域关系计算

        /// <summary>
        /// 目标点是否包含在区域内
        /// </summary>
        /// <param name="x">水平坐标</param>
        /// <param name="y">垂直坐标</param>
        /// <returns></returns>
        public virtual bool Contains(int x, int y) => Papa.Contains(x, y);

        /// <summary>
        /// 将区域放大指定量
        /// </summary>
        /// <param name="width">放大宽度</param>
        /// <param name="height">放大高度</param>
        public virtual void Inflate(int width, int height)
        {
            Papa.Inflate(width, height);
            Painter?.Paint(this);
        }

        /// <summary>
        /// 将区域替换为与目标区域的交集
        /// </summary>
        /// <param name="rect">目标区域</param>
        public virtual void Intersect(Rectangle rect)
        {
            Papa.Intersect(rect);
            Painter?.Paint(this);
        }

        /// <summary>
        /// 是否与目标区域相交
        /// </summary>
        /// <param name="rect">目标区域</param>
        /// <returns></returns>
        public virtual bool IntersectsWith(Rectangle rect) => Papa.IntersectsWith(rect);

        /// <summary>
        /// 区域是否为空
        /// </summary>
        /// <returns></returns>
        public virtual bool IsEmpty() => Papa.IsEmpty;

        /// <summary>
        /// 将区域调整指定的量
        /// </summary>
        /// <param name="point"></param>
        public virtual void Offset(Point point)
        {
            Papa.Offset(point);
            Painter?.Paint(this);
        }

        #endregion

        #region 绘制方法

        /// <summary>
        /// 注入绘制器
        /// </summary>
        /// <param name="painter"></param>
        public void SetPainter(IPaint painter)
        {
            Painter = painter ?? throw new Exception("注入空的绘制器");
        }

        /// <summary>
        /// 调用绘制器绘制
        /// </summary>
        /// <param name="painter"></param>
        [Obsolete("这是调试用方法，请直接使用 Paint() 方法调用 SetPainter() 方法已经注入的 IPaint 对象绘制", false)]
        public void Paint(IPaint painter)
        {
            if (painter == null) throw new Exception("Painter 对象为空");

            Painter?.Paint(this);
        }

        /// <summary>
        /// 绘制
        /// </summary>
        public void Paint()
        {
            if (Painter == null) throw new Exception("Painter 对象为空");

            Painter.Paint(this);
        }

        #endregion
    }
}
