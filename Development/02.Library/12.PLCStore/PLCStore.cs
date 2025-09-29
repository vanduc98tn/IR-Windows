using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Development
{
    class PLCStore
    {

        public const int D_TIME_DELAY_TOOL1 = 300;

        #region PGMain
        public const int L_BUTTON_START = 02;
        public const int L_BUTTON_STOP = 03;
        public const int L_BUTTON_HOME = 05;
        public const int L_BUTTON_RESET = 07;
        public const int L_BUTTON_LOTEND = 06;

        #endregion

        #region PG_MODEL


        public const int R_NAME_MODEL_01 = 10700;
        public const int R_NAME_MODEL_02 = 10710;
        public const int R_NAME_MODEL_03 = 10720;
        public const int R_NAME_MODEL_04 = 10730;
        public const int R_NAME_MODEL_05 = 10740;
        public const int R_NAME_MODEL_06 = 10750;
        public const int R_NAME_MODEL_07 = 10760;
        public const int R_NAME_MODEL_08 = 10770;
        public const int R_NAME_MODEL_09 = 10780;
        public const int R_NAME_MODEL_10 = 10790;

        public const int R_MODEL_SELECT_NO = 10600;
        public const int R_MODEL_COPY_TO_NO = 10602;
        public const int R_MODEL_COPY_FROM_NO = 10604;
        public const int R_MODEL_RUNNING_NO = 10610;
        public const int R_MODEL_RUNNING_NAME = 10620;


        public const int L_SAVE_MODEL = 800;
        public const int L_LOAD_MODEL = 802;
        public const int L_DELETE_MODEL = 804;
        public const int L_COPY_MODEL = 806;

        #endregion
    }
}
