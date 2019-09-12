using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadMe : MonoBehaviour {
    
    // 11.11.2018
    // 学习MovingPlatform用法，并成功添加使用。
    // 下一步计划：尝试更多变种可能！

    // 11.12.2018
    // 学习CheckPoint用法，并成功添加使用。
    // 修改了原来重载界面的重生方式, 改用Checkpoint在场景间respawn。
    // BUG：Dying动画存在问题，需要修复!
    // Dying存在连续死亡问题，需要修复！

    // 11.13.2018
    // Fixed：Dying连续死亡问题，解决！！原因：Update()的逻辑运行先后问题，给与延迟即。
    // Camera已简单修复，Cinemachine问题：尝试更多变种可能！
    // DoubleJump完成，并修复手感问题。
    // GameSession有一点小BUG，上一关的数据没法在下一关保存，可商议。

    // 11.14.2018
    // 修复了Coin拾取一次加200分的BUG，Check Collider的类型即可。

}
