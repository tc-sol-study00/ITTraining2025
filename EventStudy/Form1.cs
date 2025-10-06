using Convenience.Data;
using Convenience.Models.DataModels;
using System.ComponentModel;

namespace EventStudy {

    /// <summary>
    /// Form1イベントトリガー
    /// </summary>
    public partial class Form1 : Form {

        private readonly ConvenienceContext _context;
        private readonly Chumon _chumon;

        private BindingList<ChumonJissekiMeisai> _chumonList;

        public Form1(Chumon chumon) {
            InitializeComponent();
            _chumon = chumon;
        }
        /*
         * ボタンイベントのトリガー群
         */
        private void button1_Click(object sender, EventArgs e) {
            Handler(Kensaku);
        }
        private void button2_Click(object sender, EventArgs e)=>Handler(Save);
        private void button3_Click(object sender, EventArgs e)=>Handler(Reset);
        private void Termination_Click(object sender, EventArgs e) =>Handler(Exit);
        private void button1_ClickDummy(object sender, EventArgs e) {
            Handler(1);
        }

        /// <summary>
        /// Deligateを活用したハンドラ
        /// </summary>
        /// <param name="action">void型で引数なしメソッド</param>
        public void Handler(Action action) {
            action();
        }
        /*
         * Deligateを活用しない普通のハンドラ
         */
        public void Handler(int action) {
            switch (action) {
                case 1:
                    Kensaku();
                    break;
                case 2:
                    Save();
                    break;
                case 3:
                    Reset();
                    break;
                case 4:
                    Exit();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 検索ロジック
        /// </summary>
        public void Kensaku() {
            _chumonList = new BindingList<ChumonJissekiMeisai>(_chumon.Kensaku());
            dataGridView1.DataSource = _chumonList;
            Message.Text = _chumonList.Count + "件 見つかりました";
            dataGridView1.Refresh();
        }

        /// <summary>
        /// DB保存ロジック
        /// </summary>
        private void Save() {
            Message.Text = string.Empty;
            _chumon.Save();
            Kensaku();
            Message.Text = "保存しました";
        }

        /// <summary>
        /// 画面リセットロジックj
        /// </summary>
        private void Reset() {
            Message.Text = string.Empty;
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
        }
        /// <summary>
        /// 画面終了ロジック
        /// </summary>
        private void Exit() {
            Application.Exit();
        }
    }
}
