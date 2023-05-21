using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CheckPassword;

namespace UnitTestCheckPassword
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod_1_1_returned()
        {
            string str = "1";

            int expected = 1;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_1C_2_returned()
        {
            string str = "1C";

            int expected = 2;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_1c_2_returned()
        {
            string str = "1c";

            int expected = 2;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_1cTOCHKA_3_returned()
        {
            string str = "1c.";

            int expected = 3;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_1CTOCHKA_3_returned()
        {
            string str = "1C.";

            int expected = 3;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_1TOCHKA_2_returned()
        {
            string str = "1.";

            int expected = 2;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_111111111_2_returned()
        {
            string str = "111111111";

            int expected = 2;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_QQQQQQQQ_2_returned()
        {
            string str = "QQQQQQQQ";

            int expected = 2;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void TestMethod_qqqqqqqq_2_returned()
        {
            string str = "qqqqqqqq";

            int expected = 2;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_TOCHKA8x_2_returned()
        {
            string str = "........";

            int expected = 2;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_qqqqqqqPODCHERKIVANE_3_returned()
        {
            string str = "qqqqqqq_";

            int expected = 3;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_qqqqqq1PODCHERKIVANE_4_returned()
        {
            string str = "qqqqqq1_";

            int expected = 4;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_QQQQQQQ1PODCHERKIVANE_4_returned()
        {
            string str = "QQQQQQQ1_";

            int expected = 4;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_q_1_returned()
        {
            string str = "q";

            int expected = 1;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_qPODCHERKIVANE_2_returned()
        {
            string str = "q_";

            int expected = 2;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_Qq_2_returned()
        {
            string str = "Qq";

            int expected = 2;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_QqPODCHERKIVANE1_4_returned()
        {
            string str = "Qq_1";

            int expected = 4;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_Qq1_4_returned()
        {
            string str = "Qq_1";

            int expected = 4;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_Q_1_returned()
        {
            string str = "Q";

            int expected = 1;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_TOCHKA_1_returned()
        {
            string str = ".";

            int expected = 1;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void TestMethod_TOCHKAx7Q_3_returned()
        {
            string str = ".......Q";

            int expected = 3;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_TOCHKAx7q_3_returned()
        {
            string str = ".......q";

            int expected = 3;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_QQQQQQQq1_4_returned()
        {
            string str = "QQQQQQQq1";

            int expected = 4;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod__0_returned()
        {
            string str = "";

            int expected = 0;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_Qwer123TOCHKA_5_returned()
        {
            string str = "Qwer123.1";

            int expected = 5;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod_PROBELx8_0_returned()
        {
            string str = "        ";

            int expected = 0;

            CheckPasswordClass check = new CheckPasswordClass();

            int actual = check.CP(str);

            Assert.AreEqual(expected, actual);
        }
    }
}
