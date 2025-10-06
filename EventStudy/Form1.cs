using Convenience.Data;
using Convenience.Models.DataModels;
using System.ComponentModel;

namespace EventStudy {

    /// <summary>
    /// Form1�C�x���g�g���K�[
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
         * �{�^���C�x���g�̃g���K�[�Q
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
        /// Deligate�����p�����n���h��
        /// </summary>
        /// <param name="action">void�^�ň����Ȃ����\�b�h</param>
        public void Handler(Action action) {
            action();
        }
        /*
         * Deligate�����p���Ȃ����ʂ̃n���h��
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
        /// �������W�b�N
        /// </summary>
        public void Kensaku() {
            _chumonList = new BindingList<ChumonJissekiMeisai>(_chumon.Kensaku());
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
    }
}
