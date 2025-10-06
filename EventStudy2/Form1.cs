using Convenience.Data;
using Convenience.Models.DataModels;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.ComponentModel;

namespace EventStudy2 {

    /// <summary>
    /// Form1イベントトリガー
    /// </summary>
    public partial class Form1 : Form {

        private readonly ConvenienceContext _context;
        private readonly Chumon _chumon;

        private readonly EventHandler _eventHandler;

        private BindingList<ChumonJissekiMeisai> _chumonList;


        public Form1(Chumon chumon) {
            InitializeComponent();
            _chumon = chumon;
            _eventHandler = new EventHandler();

            // ボタンイベント登録
            this.button1.Click += (sender, e) => MenuHandler(Kensaku);
            this.button2.Click += (sender, e) => MenuHandler(Save);
            this.button3.Click += (sender, e) => MenuHandler(Reset);
            this.Termination.Click += (sender, e) => MenuHandler(Exit);
            this.button4.Click += (sender, e) => MenuHandler(SummaryProc);
            this.button5.Click += (sender, e) => MenuHandler(SummaryProcSync);
        }

        private delegate void MenuAction();
        private void MenuHandler(MenuAction action) => action();

        public void Kensaku() {
            _chumonList = new BindingList<ChumonJissekiMeisai>(_chumon.Kensaku().ToList());
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
        private async void SummaryProc() {

            Message.Text = "集計開始...";

            // タイマー準備
            System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 2000; // 2秒ごと
            _timer.Tick += Timer_Tick;  //２秒ごとに呼ばれるメソッド

            _timer.Start();     //タイマー開始

            /*
             * 集計処理（ただのループとsleep)
             */
            await _chumon.ChumonSummary();

            _timer.Stop();      //タイマー終了
            _timer.Dispose();   //タイマー削除

            Message.Text = $"処理完了！ 処理件数={_chumon.ShoriCount}";

        }

        private void SummaryProcSync() {

            // タイマー準備
            System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 2000; // 2秒ごと
            _timer.Tick += Timer_Tick;  //２秒ごとに呼ばれるメソッド

            _timer.Start();     //タイマー開始

            _chumon.ChumonSummarySync();

            _timer.Stop();      //タイマー終了

            Message.Text = $"処理完了！ 処理件数={_chumon.ShoriCount}";
            _timer.Dispose();   //タイマー削除

        }
        private void Timer_Tick(object? sender, EventArgs e) {
            Message.Text = $"処理中... 処理件数={_chumon.ShoriCount}";
        }

    }
}
