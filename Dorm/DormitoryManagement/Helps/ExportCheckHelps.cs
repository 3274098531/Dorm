using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using DormitoryManagement.Domain;
using Ionic.Zip;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using MyFramework.Helper;
using MyFramework.Helper.BirthdatyHelper;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace DormitoryManagement.Helps
{
    public class ExportCheckHelps
    {
        public static string GetSavePath(string name)
        {
            string path = IoC.Get<IConfig>().GetValue("CheckBaseFile") + "/DownLoad/" + name;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path+"/";
        }
        public static void ExportCheckResultToAcademy(IList<Academy> academylist, IList<Check> checks, string name)
        {
            foreach (Academy academy in academylist)
            {
                using (MemoryStream ms =
                    GetStreamForAcademy(checks.Where(x=>x.Room.Academy.Name==academy.Name||x.Room.Academy.ShortName == academy.ShortName).ToList(), name))
                {
                    using (var fs = new FileStream(GetSavePath(name)+ academy.Name + "反馈表.xls", FileMode.Create,
                        FileAccess.Write))
                    {
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }
                }
            }
            using (var zip = new ZipFile(Encoding.Default))
            {
                foreach (
                    Academy academy in
                        academylist.Where(academy => File.Exists(GetSavePath(name) + academy.Name + "反馈表.xls")))
                {
                    zip.AddFile(GetSavePath(name) + academy.Name + "反馈表.xls", "反馈表");
                }

                zip.Save(GetSavePath(name) + "学院反馈表.zip");
            }
            foreach (
                Academy academy in
                    academylist.Where(academy => File.Exists(GetSavePath(name) + academy.Name + "反馈表.xls")))
            {
                File.Delete(GetSavePath(name) + academy.Name + "反馈表.xls");
            }
        }
        public static MemoryStream ExportCheckResultToCheck(string name, IRepository _repository)
        { 
           
           var stream= GetMenmorstreamForCheck(name, _repository);
            using (var fs = new FileStream(GetSavePath(name) + "检查反馈表.xls",FileMode.Create,FileAccess.Write))
            {
                byte[] data = stream.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            return stream;
        }
        public static void ExportCheck(string name, IRepository repository, int acount = 8)
        {
            IList<Dorm> allDorm = repository.GetAll<Dorm>().OrderBy(x=>x.Name.Length).ThenBy(x=>x.Name).ToList();
            foreach (Dorm dorm in allDorm)
            {
                using (MemoryStream ms = GetMenmorstream(name, dorm.Name, repository))
                {
                     
                        using (
                            var fs = new FileStream(GetSavePath(name) + dorm.Name + "卫生抽查表.xls",
                                FileMode.Create,
                                FileAccess.Write))
                        {
                            byte[] data = ms.ToArray();
                            fs.Write(data, 0, data.Length);
                            fs.Flush();
                        }
                    
                }
            }
            using (var zip = new ZipFile(Encoding.Default))
            {
                foreach (Dorm dorm in allDorm)
                {

                    if (File.Exists(GetSavePath(name) + dorm.Name + "卫生抽查表.xls"))
                        {
                            zip.AddFile(GetSavePath(name) + dorm.Name + "卫生抽查表.xls");
                        }
                     
                }
                zip.Save(GetSavePath(name) + "卫生抽查表.zip");
            }
            foreach (Dorm dorm in allDorm)
            {

                if (File.Exists(GetSavePath(name) + dorm.Name + "卫生抽查表.xls"))
                    File.Delete(GetSavePath(name) + dorm.Name + "卫生抽查表.xls");
                 
            }
        }

        public static MemoryStream GetStreamForAcademy(IList<Check> checks, string name)
        {
            var book = new HSSFWorkbook();
            ISheet sheet1 = book.CreateSheet("Sheet1");
            //获取list数据 
            sheet1.SetColumnWidth(0, 3000);
            sheet1.SetColumnWidth(1, 3000);
            sheet1.SetColumnWidth(2, 3000);
            sheet1.SetColumnWidth(3, 3000);
            IRow row0 = sheet1.CreateRow(0);

            row0.CreateCell(0).SetCellValue(name);

            IRow row1 = sheet1.CreateRow(1);
            row1.CreateCell(0).SetCellValue("A");
            row1.CreateCell(1).SetCellValue("B");
            row1.CreateCell(2).SetCellValue("C");
            row1.CreateCell(3).SetCellValue("D");
            //将数据逐步写入sheet1各个行
            for (int i = 0; i < checks.Count; i++)
            {
                IRow rowtemp = sheet1.CreateRow(i + 2);
                switch (checks[i].Result.Value())
                {
                    case "A":
                        rowtemp.CreateCell(0).SetCellValue(checks[i].Room.Dorm.Name + ":" + checks[i].Room.Name);
                        break;
                    case "B":
                        rowtemp.CreateCell(1).SetCellValue(checks[i].Room.Dorm.Name + ":" + checks[i].Room.Name);
                        break;
                    case "C":
                        rowtemp.CreateCell(2).SetCellValue(checks[i].Room.Dorm.Name + ":" + checks[i].Room.Name);
                        break;
                    case "D":
                        rowtemp.CreateCell(3).SetCellValue(checks[i].Room.Dorm.Name + ":" + checks[i].Room.Name);
                        break;
                }
            }

            // 写入到客户端 
            var ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }


        public static MemoryStream GetMenmorstreamForCheck(string name, IRepository _repository)
        {
            List<Academy> allacademy = _repository.GetAll<Academy>().OrderBy(x=>x.Num).ToList();
            string filePath =  IoC.Get<IConfig>().GetValue("CheckBaseFile")+"/卫生检查反馈表模板/BaseFile.xls";
            var book = new HSSFWorkbook(File.OpenRead(filePath));
            ISheet sheet = book.GetSheetAt(0);
            ICellStyle oldstyle = sheet.GetRow(3).GetCell(0).CellStyle;
            IRow row;
            int rowcount = 4;
            try
            {
                foreach (Academy academy in allacademy)
                {
                    int countA = _repository.GetCount(new Check.ByAcademyName(academy.Name), new Check.ByInspectionName(name),
                        new Check.ByResult("A"));
                    int countB = _repository.GetCount(new Check.ByAcademyName(academy.Name), new Check.ByInspectionName(name),
                        new Check.ByResult("B"));
                    int countC = _repository.GetCount(new Check.ByAcademyName(academy.Name), new Check.ByInspectionName(name),
                        new Check.ByResult("C"));
                    int countD = _repository.GetCount(new Check.ByAcademyName(academy.Name), new Check.ByInspectionName(name),
                        new Check.ByResult("D"));
                    int countAll = _repository.GetCount(new Check.ByInspectionName(name), new Check.ByAcademyName(academy.Name));
                    string pateA = (countAll == 0 ? 0 : countA*1.0/countAll).ToString("P");
                    string pateB = (countAll == 0 ? 0 : countB*1.0/countAll).ToString("P");
                    string pateC = (countAll == 0 ? 0 : countC*1.0/countAll).ToString("P");
                    string pateD = (countAll == 0 ? 0 : countD*1.0/countAll).ToString("P");
                    row = sheet.GetRow(rowcount++);
                    row.GetCell(1).SetCellValue(countA.ToString());
                    row.GetCell(1).CellStyle = oldstyle;
                    row.GetCell(2).SetCellValue(countB.ToString());
                    row.GetCell(2).CellStyle = oldstyle;
                    row.GetCell(3).SetCellValue(countC.ToString());
                    row.GetCell(3).CellStyle = oldstyle;
                    row.GetCell(4).SetCellValue(countD.ToString());
                    row.GetCell(4).CellStyle = oldstyle;
                    row.GetCell(5).SetCellValue(countAll.ToString());
                    row.GetCell(5).CellStyle = oldstyle;
                    row = sheet.GetRow(rowcount++);
                    row.GetCell(1).SetCellValue(pateA);
                    row.GetCell(1).CellStyle = oldstyle;
                    row.GetCell(2).SetCellValue(pateB);
                    row.GetCell(2).CellStyle = oldstyle;
                    row.GetCell(3).SetCellValue(pateC);
                    row.GetCell(3).CellStyle = oldstyle;
                    row.GetCell(4).SetCellValue(pateD);
                    row.GetCell(4).CellStyle = oldstyle;
                    rowcount += 3;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            var ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public static MemoryStream GetMenmorstream(string checkname, string dormname, IRepository _repository)
        {
            string filePath =  IoC.Get<IConfig>().GetValue("CheckBaseFile")+"/卫生抽查表模板/BaseFile.xls";
        
            IList<Check> list;
            var book = new HSSFWorkbook(File.OpenRead(filePath)); 
                list = _repository.GetAll(new Check.ByDormName(dormname), new Check.ByInspectionName(checkname))
                    .OrderBy(x => x.Room.Dorm.Name.Length)
                    .ThenBy(x => x.Room.Dorm.Name)
                    .ThenBy(x => x.Room.Name)
                    .ToList(); 
            try
            {
                int count = 5;
                ISheet sheet = book.GetSheetAt(0);
                sheet.ShiftRows(count, 6, list.Count, true, true);
                IRow oldrow, row;
                oldrow = sheet.GetRow(3);
                foreach (Check check in list)
                {
                    row = sheet.CreateRow(count++);
                    row.HeightInPoints = oldrow.HeightInPoints;
                    row.CreateCell(0).SetCellValue(check.Room.Name);
                    row.GetCell(0).CellStyle = oldrow.GetCell(3).CellStyle;
                    row.CreateCell(1).SetCellValue("");
                    row.GetCell(1).CellStyle = oldrow.GetCell(3).CellStyle;
                    for (int i = 2; i <= 19; i++)
                    {
                        row.CreateCell(i);
                        row.GetCell(i).CellStyle = oldrow.GetCell(3).CellStyle;
                    }
                }
            }
            catch (Exception e)
            {
                //只在Debug模式下才输出
                throw new Exception(e.Message);
            }
            // 写入到客户端 
            var ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public static MemoryStream ExportCheckRank(string name, IRepository repository)
        {
            Inspection inspection1 = repository.GetOne(new ByName<Inspection>(name));
            var week = inspection1.CheckTime.DayOfWeek;
            var inspections = repository.GetAll<Inspection>()
                .Where(x => x.Type != inspection1.Type);
            Inspection inspection2 = null;
            foreach (var inspection in inspections)
            {
                if (inspection.CheckTime.DayOfWeek == week)
                {
                    inspection2 = inspection;
                }
            }  
             List<Check> checks1 = repository.GetAll(new Check.ByInspectionName(name)).ToList();
             List<Check> checks2 = null;
            if (inspection2 != null)
                checks2 = repository.GetAll(new Check.ByInspectionName(inspection2.Name)).ToList();
            
             List<Check> checks = new List<Check>();
              checks1.ForEach(x=>checks.Add(x));
            if(checks2!=null)
              checks2.ForEach(x=>checks.Add(x));
            IList<Academy> academies = repository.GetAll<Academy>().OrderBy(x=>x.Num).ToList();
            foreach (Academy academy in academies)
            {
                decimal account =
                    checks.Count(x => x.Room.Academy.Name == academy.Name && x.Inspection.Type == InspectionType.Routine);
                decimal randgrade = 30 - checks.Count(x => x.Inspection.Type == InspectionType.RandCheck && x.Result == Grade.D)*10;
                academy.Randgrade = randgrade > 0 ? randgrade : 0;
                decimal checkgradeA =
                    checks.Where(x => x.Inspection.Type == InspectionType.Routine && x.Room.Academy.Id == academy.Id)
                        .Count(x => x.Result.Value() == "A") ;
                decimal checkgradeB =
                    checks.Where(x => x.Inspection.Type == InspectionType.Routine && x.Room.Academy.Id == academy.Id)
                        .Count(x => x.Result.Value() == "B") ;
                decimal checkgradeD =
                    checks.Where(x => x.Inspection.Type == InspectionType.Routine && x.Room.Academy.Id == academy.Id)
                        .Count(x => x.Result.Value() == "D") ;
                if (account == 0)
                {
                    academy.Checkgrade = 0;
                }
                else
                {
                    academy.Checkgrade = (checkgradeA /account*5  + checkgradeB /account*3  -
                                          checkgradeD/account*10)*100;
                }
            }
            academies = academies.OrderByDescending(x => x.Randgrade + x.Checkgrade).ToList();

            string filePath =  IoC.Get<IConfig>().GetValue("CheckBaseFile")+"/卫生检查排名表模板/BaseFile.xls";
            var book = new HSSFWorkbook(File.OpenRead(filePath));

            ISheet sheet = book.GetSheetAt(0);
            int rowcount = 4;
            int rank = 1;
            IRow row, oldrow = sheet.GetRow(3);
            ICellStyle style = oldrow.GetCell(3).CellStyle;
            //获取list数据 
            sheet.SetColumnWidth(0, 2000);
            sheet.SetColumnWidth(1, 3000);
            sheet.SetColumnWidth(2, 3000);
            sheet.SetColumnWidth(3, 6000);
            sheet.SetColumnWidth(4, 3000);
            sheet.SetColumnWidth(5, 3000);

            try
            {
                 
                for (int i = 1; i <= academies.Count; i++)
                {
                    row = sheet.CreateRow(rowcount++);
                    if (i == 1)
                    {
                        row.CreateCell(0).SetCellValue(rank);
                         
                    }
                    else
                    {
                        if (
                             academies[i - 1].Randgrade + academies[i - 1].Checkgrade ==
                                     (academies[i - 2].Randgrade + academies[i - 2].Checkgrade) )
                        {
                            row.CreateCell(0).SetCellValue(rank);
                           
                        }
                        else
                        {
                            rank=i;
                            row.CreateCell(0).SetCellValue(i);
                        }
                    }

                    row.GetCell(0).CellStyle = style;
                    row.CreateCell(1).SetCellValue(academies[i - 1].ShortName);
                    row.GetCell(1).CellStyle = style;
                    row.CreateCell(2).SetCellValue(academies[i - 1].Checkgrade.ToString("0.00"));
                    row.GetCell(2).CellStyle = style;
                    row.CreateCell(3).SetCellValue((academies[i - 1].Checkgrade/500*70).ToString("0.00"));
                    row.GetCell(3).CellStyle = style;
                    row.CreateCell(4).SetCellValue(academies[i - 1].Randgrade.ToString("0.00"));
                    row.GetCell(4).CellStyle = style;
                    row.CreateCell(5)
                        .SetCellValue((academies[i - 1].Checkgrade/500*70 + academies[i - 1].Randgrade).ToString("0.00"));
                    row.GetCell(5).CellStyle = style;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            var ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
        public static MemoryStream ExportAllCheckByInspectionName(IList<Check>checks )
        {
             
            var book = new HSSFWorkbook();
            ISheet sheet = book.CreateSheet("Sheet1");
            int rowcount = 0;
            //获取list数据 
            sheet.SetColumnWidth(0, 6000);
            sheet.SetColumnWidth(1, 3000);
            sheet.SetColumnWidth(2, 3000);
            sheet.SetColumnWidth(3, 6000);
            sheet.SetColumnWidth(4, 3000);
            sheet.SetColumnWidth(5, 3000);
            sheet.SetColumnWidth(6, 9500);
            IRow row = sheet.CreateRow(rowcount++);
            row.CreateCell(0).SetCellValue("名称");
            row.CreateCell(1).SetCellValue("宿舍");
            row.CreateCell(2).SetCellValue("所属学院");
            row.CreateCell(3).SetCellValue("检查时间");

            row.CreateCell(4).SetCellValue("检查结果");
            try
            {
                foreach (Check check in checks)
                {
                    row = sheet.CreateRow(rowcount++);
                    row.CreateCell(0).SetCellValue(check.Inspection.Name);
                    row.CreateCell(1).SetCellValue(check.Room.Dorm.Name + check.Room.Name);
                    row.CreateCell(2).SetCellValue(check.Room.Academy.Name);
                    row.CreateCell(3).SetCellValue(check.Inspection.CheckTime.ToString());

                    row.CreateCell(4).SetCellValue(check.Result.Value());
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            var ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}