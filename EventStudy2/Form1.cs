using Convenience.Data;
using Convenience.Models.DataModels;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.ComponentModel;

namespace EventStudy2 {

    /// <summary>
    /// Form1�C�x���g�g���K�[
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

            // �{�^���C�x���g�o�^
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
            Message.Text = _chumonList.Count + "�� ������܂���";
            dataGridView1.Refresh();
        }

        /// <summary>
        /// DB�ۑ����W�b�N
        /// </summary>
        private void Save() {
            Message.Text = string.Empty;
            _chumon.Save();
            Kensaku();
            Message.Text = "�ۑ����܂���";
        }

        /// <summary>
        /// ��ʃ��Z�b�g���W�b�Nj
        /// </summary>
        private void Reset() {
            Message.Text = string.Empty;
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
        }
        /// <summary>
        /// ��ʏI�����W�b�N
        /// </summary>
        private void Exit() {
            Application.Exit();
        }
        private async void SummaryProc() {

            Message.Text = "�W�v�J�n...";

            // �^�C�}�[����
            System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 2000; // 2�b����
            _timer.Tick += Timer_Tick;  //�Q�b���ƂɌĂ΂�郁�\�b�h

            _timer.Start();     //�^�C�}�[�J�n

            /*
             * �W�v�����i�����̃��[�v��sleep)
             */
            await _chumon.ChumonSummary();

            _timer.Stop();      //�^�C�}�[�I��
            _timer.Dispose();   //�^�C�}�[�폜

            Message.Text = $"���������I ��������={_chumon.ShoriCount}";

        }

        private void SummaryProcSync() {

            // �^�C�}�[����
            System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 2000; // 2�b����
            _timer.Tick += Timer_Tick;  //�Q�b���ƂɌĂ΂�郁�\�b�h

            _timer.Start();     //�^�C�}�[�J�n

            _chumon.ChumonSummarySync();

            _timer.Stop();      //�^�C�}�[�I��

            Message.Text = $"���������I ��������={_chumon.ShoriCount}";
            _timer.Dispose();   //�^�C�}�[�폜

        }
        private void Timer_Tick(object? sender, EventArgs e) {
            Message.Text = $"������... ��������={_chumon.ShoriCount}";
        }

    }
}
