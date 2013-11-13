using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VITacademics
{
    public class Mark
    {
        //CHECK IF CBL AND LAB
        public bool iscbl, islab;

        //GET AND SET CLASSNBR
        public String classnbr;

        //ALL THE TEST MARKS FOR CBL AND LAB
        public Test[] cat = new Test[2];
        public Test[] quiz = new Test[3];

        public PBLTest[] pbl = new PBLTest[5];

        //LAB AND ASSIGNMENTS
        public Test asgn = new Test();
        public Test labcam = new Test();

        public Mark()
        {
            for (int i = 0; i < 5; i++)
            {
                pbl[i] = new PBLTest();
                if (i < 2)
                    cat[i] = new Test();
                if (i < 3)
                    quiz[i] = new Test();
            }

        }

        //FOR PBL

        //PBL CLASS
        public class PBLTest
        {
            public String title;
            public String max;
            public String wgt;
            public String conOn;
            public String status;               //REMOVE THE ONES WHICH ARE NOT REQUIRED!
            public String scdMarks;
            public String scdPercnt;
        }

        //CBL AND LAB MARKS CLASS
        public class Test
        {
            public String name;
            public String status;
            public String marks;
        }
    }
}
