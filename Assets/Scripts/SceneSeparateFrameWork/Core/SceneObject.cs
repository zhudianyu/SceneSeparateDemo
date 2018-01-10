﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 场景物件（用来包装实际用于动态加载的物体）
/// </summary>
public class SceneObject : ISceneObject
{
    /// <summary>
    /// 场景物件创建标记
    /// </summary>
    public enum CreateFlag
    {
        /// <summary>
        /// 未创建
        /// </summary>
        None,
        /// <summary>
        /// 标记为新物体
        /// </summary>
        New,
        /// <summary>
        /// 标记为旧物体
        /// </summary>
        Old,
        /// <summary>
        /// 标记为离开视野区域
        /// </summary>
        OutofBounds,
    }

    /// <summary>
    /// 场景物体加载标记
    /// </summary>
    public enum CreatingProcessFlag
    {
        None,
        /// <summary>
        /// 准备加载
        /// </summary>
        IsPrepareCreate,
        /// <summary>
        /// 准备销毁
        /// </summary>
        IsPrepareDestroy,
    }

    /// <summary>
    /// 场景物体包围盒
    /// </summary>
    public Bounds Bounds
    {
        get { return m_TargetObj.Bounds; }
    }

    public float Weight
    {
        get { return m_TargetObj.Weight; }
    }

    /// <summary>
    /// 被包装的实际用于动态加载和销毁的场景物体
    /// </summary>
    public ISceneObject TargetObj
    {
        get { return m_TargetObj; }
    }

    public CreateFlag Flag { get; set; }

    public CreatingProcessFlag ProcessFlag { get; set; }

    private ISceneObject m_TargetObj;

    public SceneObject(ISceneObject obj)
    {
        m_TargetObj = obj;
    }

    public void OnHide()
    {
        m_TargetObj.OnHide();
    }

    public bool OnShow(Transform parent)
    {
        return m_TargetObj.OnShow(parent);
    }

#if UNITY_EDITOR
    public void DrawArea(Color color, Color hitColor)
    {
        if (Flag == CreateFlag.New || Flag == CreateFlag.Old)
        {
            m_TargetObj.Bounds.DrawBounds(hitColor);
        }
        else 
        m_TargetObj.Bounds.DrawBounds(color);
    }
#endif
}
