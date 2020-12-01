using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DllPatchAok20
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            radioButtonNoWideScreen.Checked = true;
        }
        private string GamePath;
        private Byte[] exe;
        private string gameData;
        private string gamePath;
        public void Injection(UInt32 Addresse, string value)
        {
            string Value = string.Empty;
            string[] MybyteValue;
            UInt32 cpt = 0;
            Byte[] aocExe = exe;

            if (Addresse > 0x2FFFFF)
            {
                if (Addresse < 0x7A5000)
                {
                    Addresse -= 0x400000;
                }
                else
                {
                    Addresse -= 0x512000;
                }
            }

            Value = Regex.Replace(value, ".{2}", "$0 ");

            MybyteValue = Value.Split(' ');
            foreach (var item in MybyteValue)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    int val = int.Parse(item, System.Globalization.NumberStyles.HexNumber);
                    var b = aocExe[Addresse + cpt];
                    aocExe[Addresse + cpt] = (byte)val;
                    var bb = aocExe[Addresse + cpt];
                }
                cpt++;
            }
            cpt = 0;
            exe = aocExe;
        }
        public string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i-=2)
            {
                //var test = cArray[i - 1];
                //var test1 = cArray[i  ];
                reverse += cArray[i-1]+""+ cArray[i];
            }
            return reverse;
        }

        void StaticWideScreen(int x, int y, string currentDir)
        {
            //string currentDir = Directory.GetCurrentDirectory();
            var idToPick1024_768 = new List<uint>();
            var idToPick1280_1024 = new List<uint>();
            //int x = int.Parse(args[0]);
            //int y = int.Parse(args[1]);
            if (!File.Exists("resizeframes.exe"))
                File.WriteAllBytes("resizeframes.exe", DllPatchAok20.Properties.Resources.resizeframes);
            //if (!File.Exists("50500.bmp"))
            //    File.WriteAllBytes("50500.bmp", DllPatchAok20.Properties.Resources._50500);
            string drsint = Path.Combine(currentDir + @"\data", "interfac1.drs");
            string drsnew = Path.Combine(currentDir + @"\data", string.Format("{0}{1}.ws", x, y).PadLeft("1920_1080.ws".Length, '0'));
            File.Copy(Path.Combine(currentDir + @"\data", "interfac.drs"), drsint, true);
            //1024x768 interface
            idToPick1024_768.AddRange(
                new List<uint> {
                    #region 1024x768
                        51121,
                        51122,
                        51123,
                        51124,
                        51125,
                        51126,
                        51127,
                        51128,
                        51129,
                        51130,
                        51131,
                        51132,
                        51133,
                        51134,
                        51135,
                        51136,
                        51137,
                        51138,
                        #endregion 1024x768
                        
                });
            idToPick1280_1024.AddRange(
                 new List<uint> {
                     #region 1280x1024
                        51141,
                        51142,
                        51143,
                        51144,
                        51145,
                        51146,
                        51147,
                        51148,
                        51149,
                        51150,
                        51151,
                        51152,
                        51153,
                        51154,
                        51157,
                        51158,
                        51159,
                        51160,
                        #endregion

                 });
            //extract slp to Drs
            //currentDir
            DrsTable[] drsTableArray = getDrsTable(drsint);
            List<DrsItem> lstItem = new List<DrsItem>();
            lstItem = drsTableArray.Where(w => w.Type == 1936486432).First().Items.ToList();
            exctractSlpFromDrs(currentDir, idToPick1024_768, lstItem);

            foreach (uint id in idToPick1024_768)
            {
                //extract bmp to slp
                slpToBmp("int" + id + ".slp");
            }

            //resize bmp frame
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "resizeframes.exe";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            System.Diagnostics.Process proc = System.Diagnostics.Process.Start(startInfo);
            proc.WaitForExit();
            foreach (uint id in idToPick1024_768)
            {
                //overwrite slp
                //currentDir
                SlpWrite(Directory.GetCurrentDirectory(), "int" + id + "");
                //overwrite slp in drs
                //String outputname = Path.Combine(currentDir, "int" + id + ".slp");
                String outputname = Path.Combine(Directory.GetCurrentDirectory(), "int" + id + ".slp");
                byte[] data = File.ReadAllBytes(outputname);
                //if (y > 768)
                //{
                //    drsTableArray.Where(z => z.Type == 1936486432).First().Items.Where(z => z.Id == (id + 20)).First().Data = data;
                //    drsTableArray.Where(z => z.Type == 1936486432).First().Items.Where(z => z.Id == (id + 20)).First().Size = (uint)data.Length;
                //}
                //else
                //{
                    drsTableArray.Where(z => z.Type == 1936486432).First().Items.Where(z => z.Id == id).First().Data = data;
                    drsTableArray.Where(z => z.Type == 1936486432).First().Items.Where(z => z.Id == id).First().Size = (uint)data.Length;
                //}

            }

            //update slp in interfac.drs
            if (uploadSlpToDrs(drsint, drsnew, drsTableArray))
            {

                //string[] slpList = Directory.GetFiles(currentDir, "*.slp");
                string[] slpList = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.slp");
                foreach (var f in slpList)
                {
                    if (File.Exists(f))
                    {
                        try
                        {
                            File.Delete(f);
                        }
                        catch (Exception e) { };
                    }
                }
                string[] bmpList = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.bmp");
                foreach (var f in bmpList)
                {
                    if (File.Exists(f) && !f.Contains("50500.bmp"))
                    {
                        try
                        {
                            File.Delete(f);
                        }
                        catch (Exception e) { };
                    }
                }
                cleanfile();
            }

            try
            {
                if (File.Exists("resizeframes.exe"))
                    File.Delete("resizeframes.exe");
                if (File.Exists("50500.bmp"))
                    File.Delete("50500.bmp");
                Console.WriteLine("deleted");
            }
            catch (Exception e) { };
        }
        private static bool uploadSlpToDrs(string _orgDrsPath, string newDrsName, DrsTable[] drsTableArray)
        {
            using (FileStream fileStream1 = new FileStream(_orgDrsPath, FileMode.Open))
            {

                BinaryReader binaryReader = new BinaryReader(fileStream1);
                using (FileStream fileStream2 = new FileStream(newDrsName, FileMode.Create))
                {
                    bool flag = false;
                    BinaryWriter binaryWriter = new BinaryWriter(fileStream2);
                    while (true)
                    {
                        byte num = binaryReader.ReadByte();
                        binaryWriter.Write(num);
                        if (num == (byte)26)
                            flag = true;
                        else if (num > (byte)0 & flag)
                            break;
                    }
                    binaryWriter.Write(binaryReader.ReadBytes(3));
                    binaryWriter.Write(binaryReader.ReadBytes(12));
                    uint num1 = binaryReader.ReadUInt32();
                    binaryWriter.Write(num1);
                    uint num2 = binaryReader.ReadUInt32();
                    binaryWriter.Write(num2);
                    binaryReader.Close();
                    uint num4 = num2;
                    List<DrsTable> source = new List<DrsTable>();// new List<DrsTable>(drsTableArray.Length);
                    //update possitions
                    foreach (DrsTable drsTable1 in drsTableArray)
                    {
                        List<DrsItem> drsItemList = new List<DrsItem>();
                        DrsTable drsTable2 = new DrsTable()
                        {
                            Start = drsTable1.Start,
                            Type = drsTable1.Type,
                            Items = (IEnumerable<DrsItem>)drsItemList
                        };
                        foreach (DrsItem drsItem1 in drsTable1.Items)
                        {
                            DrsItem drsItem2 = new DrsItem()
                            {
                                Id = drsItem1.Id,
                                Start = num4,
                                Data = drsItem1.Data
                            };
                            drsItem2.Size = (uint)drsItem2.Data.Length;
                            num4 += drsItem2.Size;
                            drsItemList.Add(drsItem2);
                        }
                        source.Add(drsTable2);
                    }
                    foreach (DrsTable drsTable in source)
                    {
                        binaryWriter.Write(drsTable.Type);
                        binaryWriter.Write(drsTable.Start);
                        binaryWriter.Write(drsTable.Items.Count<DrsItem>());
                    }
                    foreach (DrsTable drsTable in source)
                    {
                        foreach (DrsItem drsItem in drsTable.Items)
                        {
                            binaryWriter.Write(drsItem.Id);
                            binaryWriter.Write(drsItem.Start);
                            binaryWriter.Write(drsItem.Size);
                        }
                    }
                    foreach (DrsItem drsItem in source.SelectMany<DrsTable, DrsItem>((Func<DrsTable, IEnumerable<DrsItem>>)(outTable => outTable.Items)))
                    {
                        binaryWriter.Write(drsItem.Data);
                    }
                    binaryWriter.Close();
                    fileStream2.Close();
                }
                fileStream1.Close();
            }
            return true;
        }
        private static void SlpWrite(string workingDir, string fileName)
        {
            bool transmask = true;
            bool outline1 = false;
            bool outline2 = false;
            bool plcolor = false;
            bool shadowpix = false;
            #region write slp
            slpWriter slpw = new slpWriter();
            slpw.maskfile = Path.Combine(workingDir, "mask.bmp");
            int numframes = 1;
            byte b;
            int i;
            String outputname = Path.Combine(workingDir, fileName + ".slp");
            slpw.initframes(numframes);
            string Mosaicfile = "\\" + fileName + ".bmp";
            slpw.getframe(0, workingDir, Mosaicfile, transmask, outline1, outline2, plcolor, shadowpix);
            slpw.compress();
            slpw.Write(outputname);
            #endregion
        }
        private static Bitmap slpToBmp(string SlpFileName)
        {
            #region slp export to bmp
            slpReader slpname = new slpReader();
            String workingDir = Directory.GetCurrentDirectory(); //Path.GetDirectoryName(SlpFileName);
            if (!workingDir.EndsWith("\\"))
                workingDir += "\\";
            string fi = Path.GetFileName(SlpFileName);
            Console.WriteLine("Extracting frames from " + fi + "...");
            slpname.sample =fi.Equals("int50101.slp") ?  "50532.bmp" :  "50500.bmp";
            Console.WriteLine(fi + " " + slpname.sample);
            slpname.name = fi.Split('.').First();
            slpname.Read( SlpFileName);
            slpname.save(workingDir);
            var fileName = Path.GetDirectoryName(SlpFileName) + $@"{slpname.name}.bmp";

            Bitmap bmp = (Bitmap)Bitmap.FromFile(fileName);
            #endregion
            return bmp;
        }
        private static DrsTable[] getDrsTable(string newDrsName)
        {
            List<DrsItem> lstitems = new List<DrsItem>();
            DrsTable[] drsTableArray;
            //List<DrsItem> lstItem = new List<DrsItem>();
            var idToPick = new List<uint>();

            using (FileStream fileStream1 = new FileStream(newDrsName, FileMode.Open, FileAccess.Read, FileShare.Read, 1048576, FileOptions.SequentialScan))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream1);
                bool flag = false;
                while (true)
                {
                    byte num = binaryReader.ReadByte();
                    //binaryWriter.Write(num);
                    if (num == (byte)26)
                        flag = true;
                    else if (num > (byte)0 & flag)
                        break;
                }
                binaryReader.ReadBytes(3);
                binaryReader.ReadBytes(12);
                uint num1 = binaryReader.ReadUInt32();
                uint num2 = binaryReader.ReadUInt32();

                drsTableArray = new DrsTable[(int)num1];
                for (int index = 0; (long)index < (long)num1; ++index)
                    drsTableArray[index] = new DrsTable();
                foreach (DrsTable drsTable in drsTableArray)
                {
                    drsTable.Type = binaryReader.ReadUInt32();
                    drsTable.Start = binaryReader.ReadUInt32();
                    uint num3 = binaryReader.ReadUInt32();
                    DrsItem[] drsItemArray = new DrsItem[(int)num3];
                    for (int index = 0; (long)index < (long)num3; ++index)
                        drsItemArray[index] = new DrsItem();
                    drsTable.Items = (IEnumerable<DrsItem>)drsItemArray;
                }
                foreach (DrsTable drsTable in drsTableArray)
                {
                    //Trace.Assert(fileStream1.Position == (long)drsTable.Start);
                    foreach (DrsItem drsItem in drsTable.Items)
                    {
                        drsItem.Id = binaryReader.ReadUInt32();
                        drsItem.Start = binaryReader.ReadUInt32();
                        drsItem.Size = binaryReader.ReadUInt32();
                    }
                }
                foreach (DrsItem drsItem in ((IEnumerable<DrsTable>)drsTableArray).SelectMany<DrsTable, DrsItem>((Func<DrsTable, IEnumerable<DrsItem>>)(table => table.Items)))
                {
                    //Trace.Assert(fileStream1.Position == (long)drsItem.Start);
                    drsItem.Data = binaryReader.ReadBytes((int)drsItem.Size);
                }
                //lstItem = drsTableArray.Where(w => w.Type == 1936486432).First().Items.ToList();
                binaryReader.Close();

                return drsTableArray;
            }

        }

        private static void exctractSlpFromDrs(string gameDirectory, List<uint> idToPick, List<DrsItem> lstItem)
        {
            List<DrsItem> lstitems = new List<DrsItem>();
            //get drs int
            foreach (var item in lstItem)
            {
                if (idToPick.Contains(item.Id))
                {
                    lstitems.Add(item);
                }
            }
            //extract slp for interface.drs
            foreach (DrsItem DrsItem in lstitems)
            {
                File.WriteAllBytes("int" + DrsItem.Id + ".slp", DrsItem.Data);
            }
        }
        private void buttonBrowser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.GamePath))
            {
                MessageBox.Show(@"Browser Game: Age of empire II\empires2.exe !!");
                return;
            }
            exe = File.ReadAllBytes(GamePath);
            Injection(0x600305, "E976B60100");
            Injection(0x061B980, "A3902777006891D56400FF1510C16100E97549FEFF");
            Injection(0x4f00de, "909090909090");
            Injection(0x61B9A3, "70617463682E646C6C");
            Injection(0x61B985, "68A3B96100");
            ////set full screen reolution
            ////0041BCE1     C780 F4080000 20030000     MOV DWORD PTR DS:[EAX+0x8F4],0x320
            //int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            //int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            ////Injection(0x041BCE1, "C780F408000020030000");
            ////0320 = 20030  = 800
            ////var hex = screenWidth.ToString("X").PadLeft(4, '0');
            ////var Rev = Reverse(hex);

            //string resX = Reverse(screenWidth.ToString("X").PadLeft(4, '0')).PadRight(8, '0');
            //Injection(0x041BCE7, resX);

            //string resY = Reverse(screenHeight.ToString("X").PadLeft(4, '0')).PadRight(8, '0');
            //Injection(0x041BCF4, resY);


            File.WriteAllBytes(GamePath, exe);


            string patchFile = Path.Combine(this.gamePath, "patch.dll");
            //string Btmp2SlpFile = Path.Combine(this.gamePath, "Btmp2Slp.exe");
            string wndmodedll = Path.Combine(this.gamePath, "wndmode.dll");

            #region unit formation string

            string result = "STRINGTABLE" + Environment.NewLine;
            if (File.Exists("StringTable2502.rc"))
            {
                File.Delete("StringTable2502.rc");
            }
            if (File.Exists("resource.rc"))
            {
                File.Delete("resource.rc");
            }
            if (File.Exists("StringTable2502.res"))
            {
                File.Delete("StringTable2502.res");
            }

            if (!File.Exists("ResourceHacker.exe"))
                File.WriteAllBytes("ResourceHacker.exe", DllPatchAok20.Properties.Resources.ResourceHacker);

            string currentDir = gamePath;
            string languagedll = Path.Combine(currentDir, "language.dll");
            Console.WriteLine(languagedll);
            //extract ressources from language dll to know exactly the name of language table
            var p1 = System.Diagnostics.Process.Start(@"ResourceHacker.exe", "-open \"" + languagedll + "\"  -save resource.rc  -action extract -mask STRINGTABLE,,");
            p1.WaitForExit();
            Console.WriteLine("resource.rc created");

            var fileResContent = File.ReadAllLines("resource.rc");

            string languageTable = fileResContent.ElementAt(1);

            result += languageTable + Environment.NewLine;// "LANGUAGE LANG_ENGLISH, SUBLANG_ENGLISH_US" + Environment.NewLine;
            result += "{" + Environment.NewLine;
            result += "  40016, 	\"About Face\"" + Environment.NewLine;
            result += "  40017, 	\"Wheel Right\"" + Environment.NewLine;
            result += "  40018, 	\"Wheel Left\"" + Environment.NewLine;
            result += "  40019, 	\"Horde\"" + Environment.NewLine;
            result += "  40020, 	\"Box Formation\"" + Environment.NewLine;
            result += "  40021, 	\"Line Formation\"" + Environment.NewLine;
            result += "  40022, 	\"Staggered Formation\"" + Environment.NewLine;
            result += "  40023, 	\"Flank Formation\"" + Environment.NewLine;
            result += "  40024, 	\"Triangle Formation\"" + Environment.NewLine;
            result += "  40025, 	\"Fusion Formation\"" + Environment.NewLine;
            result += "}";

            File.WriteAllText("StringTable2502.rc", result);
            Console.WriteLine("StringTable2502.rc writed");
            //compile the new combobox Random map items names
            var p2 = System.Diagnostics.Process.Start(@"ResourceHacker.exe", "-open StringTable2502.rc -save StringTable2502.res -action compile");
            p2.WaitForExit();
            //System.Threading.Thread.Sleep(200);
            //add ressources to language.dll
            var p3 = System.Diagnostics.Process.Start(@"ResourceHacker.exe", "-open \"" + languagedll + "\" -save \"" + languagedll + "\" -action addoverwrite -res StringTable2502.res -mask STRINGTABLE,,");
            p3.WaitForExit();
            Console.WriteLine("languagedll updated");
            #endregion

            #region Add rms
            string gamedata_drs = Path.Combine(gamePath, "DATA\\gamedata.drs");
            if (File.Exists(gamedata_drs))
            {
                File.Delete(gamedata_drs);
                File.WriteAllBytes(gamedata_drs, DllPatchAok20.Properties.Resources.gamedata);
            }
            if (!Directory.Exists("Random map patch\\"))
            {
                try
                {
                    //Directory.Delete("Random map patch\\", true);
                    Directory.CreateDirectory("Random map patch\\");
                    File.WriteAllBytes("Random map patch\\1 !! G  A voobly.rms", DllPatchAok20.Properties.Resources._1____G__A_voobly);
                    File.WriteAllBytes("Random map patch\\2 ARENA.bina", DllPatchAok20.Properties.Resources._2_ARENA);
                    File.WriteAllBytes("Random map patch\\3 Green Arabia.rms", DllPatchAok20.Properties.Resources._3_Green_Arabia);
                    File.WriteAllBytes("Random map patch\\4 Nomad.bina", DllPatchAok20.Properties.Resources._4_Nomad);
                    File.WriteAllBytes("Random map patch\\5 BF LORD.bina", DllPatchAok20.Properties.Resources._5__BF_LORD);
                    File.WriteAllBytes("Random map patch\\6 DE_AcropolisAok.rms", DllPatchAok20.Properties.Resources._6_DE_AcropolisAok);
                    File.WriteAllBytes("Random map patch\\7 DE_Cross.rms", DllPatchAok20.Properties.Resources._7_DE_CrossAok);
                    File.WriteAllBytes("Random map patch\\8 DE_Hideout.rms", DllPatchAok20.Properties.Resources._8_DE_HideoutAok);
                    File.WriteAllBytes("Random map patch\\9 DE_Hillfort.rms", DllPatchAok20.Properties.Resources._9_DE_HillfortAok);
                    File.WriteAllBytes("Random map patch\\10 DE_Lombardia.rms", DllPatchAok20.Properties.Resources._10_DE_LombardiaAok);
                    File.WriteAllBytes("Random map patch\\11 DE_Valley.rms", DllPatchAok20.Properties.Resources._11_DE_ValleyAok);
                    File.WriteAllBytes("Random map patch\\12 DE_Golden_Pit.rms", DllPatchAok20.Properties.Resources._12_DE_Golden_Pitaok);
                    File.WriteAllBytes("Random map patch\\13 NAC_Land_Madness.rms", DllPatchAok20.Properties.Resources._13_NAC_Land_Madnessaok);
                    File.WriteAllBytes("Random map patch\\14 HC_Chaos_Pit.rms", DllPatchAok20.Properties.Resources._14_HC_Chaos_Pitaok);
                    File.WriteAllBytes("Random map patch\\15 BO_Scandinavia.rms", DllPatchAok20.Properties.Resources._15_BO_Scandinavia_aok);
                    File.WriteAllBytes("Random map patch\\16 CDC_Arina.rms", DllPatchAok20.Properties.Resources._16_CDC_Arina_aok);
                    File.WriteAllBytes("Random map patch\\17 CDC_El_Dorado.rms", DllPatchAok20.Properties.Resources._17_CDC_El_Dorado_aok);
                    File.WriteAllBytes("Random map patch\\18 SHERWOOD FOREST.rms", DllPatchAok20.Properties.Resources._18_SHERWOOD_FOREST);
                    File.WriteAllBytes("Random map patch\\19 ES_The_Unknown.rms", DllPatchAok20.Properties.Resources._19_ES_The_Unknown);
                    File.WriteAllBytes("Random map patch\\20 MICHI.rms", DllPatchAok20.Properties.Resources._20_MICHI);
                    File.WriteAllBytes("Random map patch\\21 ForestNothing.rms", DllPatchAok20.Properties.Resources._21_ForestNothing);
                }
                catch (Exception ex) { };

            }else
            {
                try
                {
                    //care big name crash the game 
                    Directory.Delete("Random map patch\\", true);
                    Directory.CreateDirectory("Random map patch\\");
                    File.WriteAllBytes("Random map patch\\1 !! G  A voobly.rms", DllPatchAok20.Properties.Resources._1____G__A_voobly);
                    File.WriteAllBytes("Random map patch\\2 ARENA.bina", DllPatchAok20.Properties.Resources._2_ARENA);
                    File.WriteAllBytes("Random map patch\\3 Green Arabia.rms", DllPatchAok20.Properties.Resources._3_Green_Arabia);
                    File.WriteAllBytes("Random map patch\\4 Nomad.bina", DllPatchAok20.Properties.Resources._4_Nomad);
                    File.WriteAllBytes("Random map patch\\5 BF LORD.bina", DllPatchAok20.Properties.Resources._5__BF_LORD);
                    File.WriteAllBytes("Random map patch\\6 DE_AcropolisAok.rms", DllPatchAok20.Properties.Resources._6_DE_AcropolisAok);
                    File.WriteAllBytes("Random map patch\\7 DE_Cross.rms", DllPatchAok20.Properties.Resources._7_DE_CrossAok);
                    File.WriteAllBytes("Random map patch\\8 DE_Hideout.rms", DllPatchAok20.Properties.Resources._8_DE_HideoutAok);
                    File.WriteAllBytes("Random map patch\\9 DE_Hillfort.rms", DllPatchAok20.Properties.Resources._9_DE_HillfortAok);
                    File.WriteAllBytes("Random map patch\\10 DE_Lombardia.rms", DllPatchAok20.Properties.Resources._10_DE_LombardiaAok);
                    File.WriteAllBytes("Random map patch\\11 DE_Valley.rms", DllPatchAok20.Properties.Resources._11_DE_ValleyAok);
                    File.WriteAllBytes("Random map patch\\12 DE_Golden_Pit.rms", DllPatchAok20.Properties.Resources._12_DE_Golden_Pitaok);
                    File.WriteAllBytes("Random map patch\\13 NAC_Land_Madness.rms", DllPatchAok20.Properties.Resources._13_NAC_Land_Madnessaok);
                    File.WriteAllBytes("Random map patch\\14 HC_Chaos_Pit.rms", DllPatchAok20.Properties.Resources._14_HC_Chaos_Pitaok);
                    File.WriteAllBytes("Random map patch\\15 BO_Scandinavia.rms", DllPatchAok20.Properties.Resources._15_BO_Scandinavia_aok);
                    File.WriteAllBytes("Random map patch\\16 CDC_Arina.rms", DllPatchAok20.Properties.Resources._16_CDC_Arina_aok);
                    File.WriteAllBytes("Random map patch\\17 CDC_El_Dorado.rms", DllPatchAok20.Properties.Resources._17_CDC_El_Dorado_aok);
                    File.WriteAllBytes("Random map patch\\18 SHERWOOD FOREST.rms", DllPatchAok20.Properties.Resources._18_SHERWOOD_FOREST);
                    File.WriteAllBytes("Random map patch\\19 ES_The_Unknown.rms", DllPatchAok20.Properties.Resources._19_ES_The_Unknown);
                    File.WriteAllBytes("Random map patch\\20 MICHI.rms", DllPatchAok20.Properties.Resources._20_MICHI);
                    File.WriteAllBytes("Random map patch\\21 ForestNothing.rms", DllPatchAok20.Properties.Resources._21_ForestNothing);
                }
                catch (Exception ex) { };
            }
            List<DrsTable> lstDrsGameData = new List<DrsTable>();
            DrsItem newDrsItem = null;
            byte[] dt;
            uint newMapId = 10000;
            int cpt = 10874;
            var lstf = Directory.GetFiles("Random map patch\\").Select(w => w.Replace(@"Random map\", "").Split('.').First()).ToList();
            //string[] numbers = Regex.Split(input, @"\D+");

            var lstfile = Directory.GetFiles("Random map patch\\").ToList();
            int i = cpt - lstf.Count;
            string fileGamedataDrs = Path.Combine(currentDir + @"\Data", "gamedata.drs");
            var tmpDrsFile = fileGamedataDrs.Replace(".drs", "Tmp.drs");
            if (File.Exists("StringTable680.rc"))
            {
                File.Delete("StringTable680.rc");
            }
            if (File.Exists("resource.rc"))
            {
                File.Delete("resource.rc");
            }
            if (File.Exists("StringTable680.res"))
            {
                File.Delete("StringTable680.res");
            }

            //extract ressources from language dll to know exactly the name of language table
            var p4 = System.Diagnostics.Process.Start(@"ResourceHacker.exe", "-open \"" + languagedll + "\"  -save resource.rc  -action extract -mask STRINGTABLE,,");
            p4.WaitForExit();
            fileResContent = File.ReadAllLines("resource.rc");
            languageTable = fileResContent.ElementAt(1);
            //todo allow user to change language
            result = "STRINGTABLE" + Environment.NewLine;
            result += languageTable + Environment.NewLine;// "LANGUAGE LANG_ENGLISH, SUBLANG_ENGLISH_US" + Environment.NewLine;
            result += "{" + Environment.NewLine;
            if (File.Exists(fileGamedataDrs))
            {
                lstDrsGameData = LoadDrsInList(fileGamedataDrs);
                var lastItem = lstDrsGameData.Where(w => w.Type == 1651076705).First().Items.Last();
                List<DrsItem> itemList = lstDrsGameData.Where(w => w.Type == 1651076705).First().Items.ToList();
                //Add map in game data drs
                foreach (var f in lstfile.OrderByDescending(x=>x))
                {
                    dt = File.ReadAllBytes(f);
                    newDrsItem = new DrsItem()
                    {
                        Id = newMapId,
                        //Start = lastItem.Start+ lastItem.Size,
                        Data = dt
                    };
                    itemList.Add(newDrsItem);
                    newMapId++;
                    i++;
                    string item = f.Replace(@"Random map patch\", "").Split('.').First();
                    result += "" + i + ", 	" + "\"" + item + "\"" + Environment.NewLine;

                    //ResourceHacker.exe  -open StringTable680.rc -save StringTable680.res -action compile
                    //ResourceHacker.exe -open language.dll -save language1.dll -action addoverwrite -res StringTable680.res -mask STRINGTABLE,,

                }
                itemList.Where(w => w.Id == 54000).First().Data = DllPatchAok20.Properties.Resources._54000;
                result += "10875, 	\"Arabia\"" + Environment.NewLine;
                result += "10876, 	\"Archipelago\"" + Environment.NewLine;
                result += "10877, 	\"Baltic\"" + Environment.NewLine;
                result += "10878, 	\"Black Forest\"" + Environment.NewLine;
                result += "10879, 	\"Coastal\"" + Environment.NewLine;

                result += "}";
                File.WriteAllText("StringTable680.rc", result);
                //compile the new combobox Random map items names
                var p5 = System.Diagnostics.Process.Start(@"ResourceHacker.exe", "-open StringTable680.rc -save StringTable680.res -action compile");
                p5.WaitForExit();
                //add ressources to language.dll
                var p6 = System.Diagnostics.Process.Start(@"ResourceHacker.exe", "-open \"" + languagedll + "\" -save \"" + languagedll + "\" -action addoverwrite -res StringTable680.res -mask STRINGTABLE,,");
                p6.WaitForExit();
                lstDrsGameData.Where(w => w.Type == 1651076705).First().Items = itemList;
                saveDrsFromLis(fileGamedataDrs, tmpDrsFile, lstDrsGameData);
                if (File.Exists(fileGamedataDrs))
                {
                    //update Drs file
                    File.Copy(tmpDrsFile, fileGamedataDrs, true);
                    File.Delete(tmpDrsFile);
                }
                #endregion
                if(radioButtonWideScreenCentred.Checked)
                {
                    File.WriteAllBytes(patchFile, DllPatchAok20.Properties.Resources.Patch);
                }
                if (radioButtonWideScreenVoobly.Checked)
                {
                    File.WriteAllBytes(patchFile, DllPatchAok20.Properties.Resources.patchvooblyRes);
                }
                if(radioButtonNoWideScreen.Checked)
                {
                    File.WriteAllBytes(patchFile, DllPatchAok20.Properties.Resources.patchNowideScreen);
                }

                if (!File.Exists(wndmodedll) && checkBoxWindowedMod.Checked)
                {
                    File.WriteAllBytes(wndmodedll, DllPatchAok20.Properties.Resources.wndmode);
                }
                if(radioButtonWideScreen1280.Checked)
                {
                    int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                    int screenHeight = Screen.PrimaryScreen.Bounds.Height;
                    StaticWideScreen(screenWidth, screenHeight, currentDir);
                    File.WriteAllBytes("aok20a_offsets", DllPatchAok20.Properties.Resources.aok20a_offsets);
                    File.WriteAllBytes("patcher.exe", DllPatchAok20.Properties.Resources.patcher);
                    File.Copy(currentDir + "\\empires2.exe", "empires2.exe");
                    var p7 = System.Diagnostics.Process.Start(@"patcher.exe", "empires2.exe aok20a_offsets ");
                    p7.WaitForExit();
                    File.Delete(currentDir + "\\empires2.exe");
                    File.Copy("empires2_"+screenWidth+"x"+screenHeight+".exe", currentDir + "\\empires2.exe");
                    File.WriteAllBytes(patchFile, DllPatchAok20.Properties.Resources.patchNowideScreen);
                }
                cleanfile();
                MessageBox.Show("Done.");
            }
        }

        void cleanfile()
        {
            string[] frc = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.rc");
            foreach (var f in frc)
            {
                if (File.Exists(f))
                {
                    try
                    {
                        File.Delete(f);
                    }
                    catch (Exception e) { };
                }
            }
            string[] fres = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.res");
            foreach (var f in fres)
            {
                if (File.Exists(f))
                {
                    try
                    {
                        File.Delete(f);
                    }
                    catch (Exception e) { };
                }
            }
            string[] fempires2exe = Directory.GetFiles(Directory.GetCurrentDirectory(), "empires2*.exe");
            foreach (var f in fempires2exe)
            {
                if (File.Exists(f))
                {
                    try
                    {
                        File.Delete(f);
                    }
                    catch (Exception e) { };
                }
            }
            try
            {
                File.Delete("ResourceHacker.exe");
                File.Delete("patcher.exe");
                File.Delete("ResourceHacker.ini");
                File.Delete("aok20a_offsets");
            }
            catch (Exception e) { };
        }

        private static List<DrsTable> LoadDrsInList(string drsPathFile)
        {
            //List<DrsItem> lstitems = new List<DrsItem>();
            DrsTable[] drsTableArray = new DrsTable[0];
            List<DrsItem> lstItem = new List<DrsItem>();
            if (File.Exists(drsPathFile))
            {
                using (FileStream fileStream1 = new FileStream(drsPathFile, FileMode.Open))//, FileSystemRights.ReadData, FileShare.Read, 1048576, FileOptions.SequentialScan))
                {
                    BinaryReader binaryReader = new BinaryReader(fileStream1);
                    bool flag = false;
                    while (true)
                    {
                        byte num = binaryReader.ReadByte();
                        if (num == (byte)26)
                            flag = true;
                        else if (num > (byte)0 & flag)
                            break;
                    }
                    binaryReader.ReadBytes(3);
                    binaryReader.ReadBytes(12);
                    uint num1 = binaryReader.ReadUInt32();
                    uint num2 = binaryReader.ReadUInt32();

                    drsTableArray = new DrsTable[(int)num1];
                    for (int index = 0; (long)index < (long)num1; ++index)
                        drsTableArray[index] = new DrsTable();
                    foreach (DrsTable drsTable in drsTableArray)
                    {
                        drsTable.Type = binaryReader.ReadUInt32();
                        drsTable.Start = binaryReader.ReadUInt32();
                        uint num3 = binaryReader.ReadUInt32();
                        DrsItem[] drsItemArray = new DrsItem[(int)num3];
                        for (int index = 0; (long)index < (long)num3; ++index)
                            drsItemArray[index] = new DrsItem();
                        drsTable.Items = (IEnumerable<DrsItem>)drsItemArray;
                    }
                    foreach (DrsTable drsTable in drsTableArray)
                    {
                        //Trace.Assert(fileStream1.Position == (long)drsTable.Start);
                        foreach (DrsItem drsItem in drsTable.Items)
                        {
                            drsItem.Id = binaryReader.ReadUInt32();
                            drsItem.Start = binaryReader.ReadUInt32();
                            drsItem.Size = binaryReader.ReadUInt32();
                        }
                    }
                    foreach (DrsItem drsItem in ((IEnumerable<DrsTable>)drsTableArray).SelectMany<DrsTable, DrsItem>((Func<DrsTable, IEnumerable<DrsItem>>)(table => table.Items)))
                    {
                        //Trace.Assert(fileStream1.Position == (long)drsItem.Start);
                        drsItem.Data = binaryReader.ReadBytes((int)drsItem.Size);
                    }
                    //where type is slp not .way or .bina
                    //lstItem = drsTableArray.Where(w => w.Type == 1936486432).First().Items.ToList();
                    binaryReader.Close();
                }
            }
            return drsTableArray.ToList();

        }
        private static void saveDrsFromLis(string drsPathFile, string newDrsFile, List<DrsTable> lstDrsTable)
        {


            using (FileStream fileStream1 = new FileStream(drsPathFile, FileMode.Open))
            {

                BinaryReader binaryReader = new BinaryReader(fileStream1);
                using (FileStream fileStream2 = new FileStream(newDrsFile, FileMode.Create))
                {
                    bool flag = false;
                    BinaryWriter binaryWriter = new BinaryWriter(fileStream2);
                    string res = string.Empty;
                    while (true)
                    {
                        byte num = binaryReader.ReadByte();
                        binaryWriter.Write(num);
                        res += num;
                        if (num == (byte)26)
                            flag = true;
                        else if (num > (byte)0 & flag)
                            break;
                    }
                    var tb = binaryReader.ReadBytes(3);
                    binaryWriter.Write(tb);
                    var db = binaryReader.ReadBytes(12);
                    binaryWriter.Write(db);
                    //nb of type exemple :3 slp bina way
                    uint num1 = binaryReader.ReadUInt32();
                    binaryWriter.Write(num1);
                    //first start position drs item
                    uint num2 = binaryReader.ReadUInt32();
                    uint nume2_ = getfirstStartDrs(lstDrsTable);
                    binaryWriter.Write(nume2_);
                    //binaryWriter.Write(num2);

                    uint num4 = nume2_;
                    //uint num4 = num2;
                    List<DrsTable> source = new List<DrsTable>();
                    uint id;
                    //need update number of item
                    //uint num3 = 0;

                    //update possitions
                    foreach (DrsTable drsTable1 in lstDrsTable)
                    {

                        List<DrsItem> drsItemList = new List<DrsItem>();
                        DrsTable drsTable2 = new DrsTable()
                        {
                            Start = drsTable1.Start,
                            Type = drsTable1.Type,
                            Items = (IEnumerable<DrsItem>)drsItemList
                        };
                        foreach (DrsItem drsItem1 in drsTable1.Items)
                        {
                            DrsItem drsItem2 = new DrsItem()
                            {
                                Id = drsItem1.Id,
                                Start = num4,
                                Data = drsItem1.Data
                            };
                            drsItem2.Size = (uint)drsItem2.Data.Length;
                            num4 += drsItem2.Size;
                            drsItemList.Add(drsItem2);
                        }
                        source.Add(drsTable2);
                    }
                    //binaryWriter.Write(num3);
                    binaryReader.Close();
                    //start 100
                    //126*1512  //126 = item cout
                    //1512+100 =1612
                    //224*12 =2688‬   //224 = item count
                    //2688+1612 = 4300‬
                    uint precStart = 0;
                    uint result = 0;
                    uint itemCount = 0;
                    uint countallItem = 0;
                    uint firstStart = 0;
                    DrsTable precDrsTable = new DrsTable();
                    foreach (DrsTable drsTable in source)
                    {
                        binaryWriter.Write(drsTable.Type);
                        if (precStart == 0)
                        {
                            binaryWriter.Write(drsTable.Start);
                            firstStart = drsTable.Start;
                        }
                        else
                        {
                            itemCount = (uint)precDrsTable.Items.Count<DrsItem>();
                            result = (uint)itemCount * 12 + precStart;
                            binaryWriter.Write(result);
                        }
                        uint count = (uint)drsTable.Items.Count<DrsItem>();
                        binaryWriter.Write(count);
                        countallItem += count;
                        precStart = drsTable.Start;
                        precDrsTable = drsTable;
                    }
                    precStart = 0;
                    uint num_ = num2;
                    //4264 221
                    //4864
                    //+3 SLP+> 4900
                    //4888
                    //4900 -4888 + 12
                    //conclusion num2 = 12*(126+223+50) = 4 788‬ +100

                    num_ = (uint)12 * countallItem + firstStart;
                    foreach (DrsTable drsTable in source)
                    {
                        foreach (DrsItem drsItem in drsTable.Items)
                        {
                            binaryWriter.Write(drsItem.Id);
                            binaryWriter.Write(drsItem.Start);
                            binaryWriter.Write(drsItem.Size);
                        }
                    }
                    foreach (DrsItem drsItem in source.SelectMany<DrsTable, DrsItem>((Func<DrsTable, IEnumerable<DrsItem>>)(outTable => outTable.Items)))
                    {
                        binaryWriter.Write(drsItem.Data);
                    }
                    binaryWriter.Close();
                    fileStream2.Close();
                }
                fileStream1.Close();
            }
        }
        private static uint getfirstStartDrs(List<DrsTable> lstDrsTable)
        {
            uint res = 0;
            uint precStart = 0;
            uint result = 0;
            uint itemCount = 0;
            uint countallItem = 0;
            uint firstStart = 0;
            DrsTable precDrsTable = new DrsTable();

            foreach (DrsTable drsTable in lstDrsTable)
            {

                if (precStart == 0)
                {
                    firstStart = drsTable.Start;
                }
                else
                {
                    itemCount = (uint)precDrsTable.Items.Count<DrsItem>();
                    result = (uint)itemCount * 12 + precStart;

                }
                uint count = (uint)drsTable.Items.Count<DrsItem>();
                countallItem += count;
                precStart = drsTable.Start;
                precDrsTable = drsTable;
            }
            res = (uint)12 * countallItem + firstStart;
            return res;
        }
        private void BtnPatch_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter =
            "empires2 exe (*.exe)|*.exe";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.GamePath = openFileDialog.FileName;
                this.gamePath = new FileInfo(this.GamePath).Directory.FullName;
                this.gameData = new FileInfo(this.GamePath).Directory.FullName + "\\data";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
