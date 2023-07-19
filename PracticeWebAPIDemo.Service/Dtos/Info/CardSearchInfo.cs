using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWebAPIDemo.Service.Dtos.Info
{
    public class CardSearchInfo
    {
        /// <summary>
        /// 卡片名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 攻擊力下限
        /// </summary>
        public int? MinAttack { get; set; }

        /// <summary>
        /// 攻擊力上限
        /// </summary>
        public int? MaxAttack { get; set; }

        /// <summary>
        /// 血量下限
        /// </summary>
        public int? MinHealth { get; set; }

        /// <summary>
        /// 血量上限
        /// </summary>
        public int? MaxHealth { get; set; }

        /// <summary>
        /// 花費值下限
        /// </summary>
        public int? MinCost { get; set; }

        /// <summary>
        /// 花費值上限
        /// </summary>
        public int? MaxCost { get; set; }
    }
}
