using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ToolGood.Words;

namespace Devexpress_ComboBoxEdit
{
    /// <summary>
    /// CommboxEditor过滤设置（支持中文、拼音、五笔过滤）
    /// author:zhaixd
    /// date:2017.08.03
    /// </summary>
    public partial class Form1 : Form
    {
        #region 自定义属性
        //使用所有国家作为临时数据源
        private readonly string countrys = "阿根廷,澳大利亚,比利时,玻利维亚,巴西,白俄罗斯,加拿大,智利,中国,哥伦比亚,哥斯达黎加,古巴,捷克斯洛伐克,丹麦,多米尼加共和国,厄瓜多尔,埃及,萨尔瓦多,埃塞俄比亚,法国,希腊,危地马拉,海地,洪都拉斯,印度,伊朗,伊拉克,黎巴嫩,利比里亚,卢森堡,墨西哥,荷兰,新西兰,尼加拉瓜,挪威,巴拿马,巴拉圭,秘鲁,菲律宾,波兰,俄罗斯联邦,沙特阿拉伯,南非,阿拉伯叙利亚共和国,土耳其,乌克兰,大不列颠及北爱尔兰联合王国,美利坚合众国,乌拉圭,委内瑞拉,南斯拉夫,阿富汗,冰岛,瑞典,泰国,巴基斯坦,也门,缅甸,以色列,印度尼西亚,阿尔巴尼亚,澳地利,保加利亚,柬埔寨,芬兰,匈牙利,爱尔兰,意大利,约旦,老挝人民民主共和国,罗马利亚,西班牙,斯里兰卡,阿拉伯利比亚民众国,尼泊尔,葡萄牙,日本,摩洛哥,苏丹,突尼斯,加纳,马来西亚,几内亚,贝宁,布基纳法索,喀麦隆,中非共和国,乍得,刚果,科特迪瓦,塞浦路斯,加蓬,马达加斯加,马里,尼日尔,尼日利亚,塞内加尔,索马里,多哥,刚果民主共和国,毛里塔尼亚,蒙古,塞拉利昂,坦桑尼亚联合共和国,阿尔及利亚,布隆迪,牙买加,卢旺达,特立尼达和多巴哥,乌干达,肯尼亚,科威特,马拉维,马耳他,赞比亚,冈比亚,马尔代夫,新加坡,巴巴多斯,博茨瓦纳,圭亚那,莱索托,民主也门,赤道几内亚,毛里求斯,斯威士兰,斐济,巴林,不丹,阿曼,卡塔尔,阿拉伯联合酋长国,巴哈马,德意志联邦共和国,德意志民主共和国,孟加拉国,格林纳达,几内亚比绍,佛得角,科摩罗,莫桑比克,巴布亚新几内亚,圣多美和普林西比多米尼加,所罗门群岛,苏里南,安哥拉,萨摩亚,塞舌尔,吉布提,越南,圣卢西亚,圣文森特和格林纳丁斯,津巴布韦,安提瓜和巴布达,伯利兹,瓦努阿图,圣基茨和尼维斯,文莱达鲁萨兰国,列支敦士登,纳米比亚,朝鲜民主主义人民共和国,爱沙尼亚,密克罗尼西亚联邦,拉脱维亚,立陶宛,马绍尔群岛,大韩民国,亚美尼亚,阿塞拜疆,波斯尼亚和黑塞哥维那,克罗地亚,格鲁吉亚,哈萨克斯坦,吉尔吉斯,摩尔多瓦,圣马力诺,斯洛文尼亚,塔吉克斯坦,土库曼斯坦,乌兹别克斯坦,安道尔,捷克共和国,厄立特里亚,摩纳哥,斯洛伐克共和国,前南斯拉夫的马其顿共和国,帕劳,基里巴斯共和国,瑙鲁,汤加,图瓦卢,南斯拉夫,瑞士,东帝汶";
        /// <summary>
        /// 自定义数据源
        /// </summary>
        private DataTable dtSource { get; set; }

        private List<object> cmbDefaultBindSource { get; set; }
        /// <summary>
        /// 过滤模式 中文,拼音首字母，拼音全称
        /// </summary>
        private string filterType
        {
            get
            {
                var filter = this.cmbFilterMode.SelectedText.ToString();
                if (string.IsNullOrWhiteSpace(filter))
                    filter = "中文";
                return filter;
            }
        }
        /// <summary>
        /// 当前是否已选择正确的选项，用于隐藏弹窗
        /// </summary>
        private bool isMatch { get; set; }

        #endregion

        public Form1()
        {
            InitializeComponent();
            //设置下拉控件的属性
            this.cmbox.Properties.AutoComplete = false;
            this.cmbox.Properties.DropDownItemHeight = 28;

            this.timer1.Enabled = false;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new EventHandler(timer1_Tick);
            
        }

        #region 事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.progressPanel1.Visible = true;
            timer1.Start();
        }

        /// <summary>
        /// 延迟初始化数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            initData();
            timer1.Stop();
            timer1.Enabled = false;
            this.progressPanel1.Visible = false;
        }
        
        /// <summary>
        /// 点击某项后隐藏弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void cmbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = cmbox.Text.ToString();
            listMessages.Items.Add(string.Format("SelectedIndexChanged事件触发，当前输入的内容：{0}。", str));
            if (isMatch)
            {
                cmbox.ClosePopup();
            }
        }
        
        /// <summary>
        /// 手动点击下拉框时，清空当前过滤的信息，展示所有的选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (isMatch)
            {
                string str = cmbox.Text.ToString();
                listMessages.Items.Add(string.Format("ButtonClick事件触发，当前输入的内容：{0}。", str));
                //cmbox.ClosePopup();
                cmbox.Properties.Items.Clear();//清空原来的值
                cmbox.Properties.Items.AddRange(cmbDefaultBindSource);
                cmbox.ShowPopup();
                isMatch = false;
            }
        }
        
        /// <summary>
        /// 清空日志信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.listMessages.Items.Clear();
        }

        /// <summary>
        /// 输入值改变触发数据源过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbox_EditValueChanged(object sender, EventArgs e)
        {
            string str = cmbox.Text.ToString();
            listMessages.Items.Add(string.Format("EditValueChanged事件触发，当前输入的内容：{0}。", str));
            try
            {
                List<object> bindSource = new List<object>();
                cmbox.ClosePopup();
                cmbox.Properties.Items.Clear();//清空原来的值
                isMatch = false;
                if (string.IsNullOrWhiteSpace(str))
                {
                    bindSource.AddRange(cmbDefaultBindSource);
                }
                else
                {
                    //完全匹配查找，是否已选择正确的值
                    DataView dtView = dtSource.DefaultView;
                    dtView.RowFilter = string.Format("Name = '{0}'", str);
                    DataTable dtFilter = dtView.ToTable();
                    if (dtFilter.Rows.Count == 1)
                    {
                        bindSource.Add(dtFilter.Rows[0]["Name"].ToString());
                        isMatch = true;
                    }
                    else 
                    {
                        string s = getFilterSql(str);
                        dtView.RowFilter = s;
                        //过滤后的数据源
                        dtFilter = dtView.ToTable();
                        if (dtFilter.Rows.Count <= 0)
                        {
                            bindSource.Add("没有匹配的数据");
                        }
                        else
                        {
                            for (int i = 0; i < dtFilter.Rows.Count; i++)
                            {
                                bindSource.Add(dtFilter.Rows[i]["Name"].ToString());
                            }
                        }
                    }
                }
                cmbox.Properties.Items.AddRange(bindSource.ToArray());

                cmbox.ShowPopup();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 自定义方法
        /// <summary>
        /// 初始化数据源并绑定
        /// </summary>
        private void initData()
        {
            try
            {
                isMatch = false;
                //初始化设置五笔字典的文件路径
                WBTranslationHelper.Init();

                #region 初始化数据源
                cmbDefaultBindSource = new List<object>();
                DataTable dt = new DataTable();
                dt.Columns.Add("Code", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("PinYin", typeof(string)); //处理中文转拼音，用于过滤使用
                dt.Columns.Add("FirstPinYin", typeof(string)); //处理中文转拼音(首字符)，用于过滤使用
                dt.Columns.Add("Wb", typeof(string)); //处理中文转五笔编码，用于过滤使用
                dt.Columns.Add("FirstWb", typeof(string)); //处理中文转五笔编码(首字符)，用于过滤使用
                var countryArr = countrys.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < countryArr.Length; i++)
                {
                    var name = countryArr[i];
                    // 获取拼音全拼
                    var name_PinYin = WordsHelper.GetPinYin(name);
                    // 获取首字母
                    var name_First_PinYin = WordsHelper.GetFirstPinYin(name);
                    // 获取五笔全拼
                    var name_Wb = WBTranslationHelper.GetWbAll(name);
                    // 获取五笔编码首字母
                    var name_First_Wb = WBTranslationHelper.GetWbFirst(name);
                    dt.Rows.Add((i + 1).ToString(), name, name_PinYin, name_First_PinYin, name_Wb, name_First_Wb);
                    cmbDefaultBindSource.Add(name);
                }
                dt.AcceptChanges();
                dtSource = dt;
                #endregion

                //绑定数据源
                this.cmbox.Properties.Items.AddRange(cmbDefaultBindSource);

                //绑定过滤模式
                this.cmbFilterMode.Properties.Items.Add("中文");
                this.cmbFilterMode.Properties.Items.Add("拼音首字母");
                this.cmbFilterMode.Properties.Items.Add("拼音全拼");
                this.cmbFilterMode.Properties.Items.Add("五笔首字母");
                this.cmbFilterMode.Properties.Items.Add("五笔全拼");
                this.cmbFilterMode.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取过滤的条件
        /// </summary>
        /// <param name="value">要过滤的值</param>
        /// <returns></returns>
        private string getFilterSql(string value)
        {
            string filterStr = string.Format("Name like '%{0}%'", value);
            switch (filterType)
            {
                case "中文":
                    filterStr = string.Format("Name like '%{0}%'", value);
                    break;
                case "拼音首字母":
                    filterStr = string.Format("FirstPinYin like '%{0}%'", value);
                    break;
                case "拼音全拼":
                    filterStr = string.Format("PinYin like '%{0}%'", value);
                    break;
                case "五笔首字母":
                    filterStr = string.Format("FirstWb like '%{0}%'", value);
                    break;
                case "五笔全拼":
                    filterStr = string.Format("Wb like '%{0}%'", value);
                    break;
                default:
                    break;
            }
            return filterStr;
        }
        #endregion
        
    }
}
