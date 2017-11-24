using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using DormitoryManagement.Domain;
using MyFramework.Application.Interface;
using MyFramework.Application.InterfaceAchieve;
using MyFramework.Helper;

namespace DormitoryManagement.Application.Imp
{
    public class InspectionCommand:IInspectionCommand
    {
        public InspectionCommand(Inspection inspection, IRepository repository)
        {
            Inspection = inspection;
            Repository = repository;
        }

        public Inspection Inspection { get; set; }

        public IRepository Repository { get; set; }
        public IInspectionCommand CheckTime(string checktime)
        {
            Inspection.CheckTime = DateTime.Parse(checktime);
            return this;
        }

        public IInspectionCommand Name(string name)
        {
            Inspection.Name = name;
            return this;
        }

        public IInspectionCommand Type(string type)
        {
            Inspection.Type = type.To<InspectionType>();
            return this;
        }

        public IInspectionCommand Ratio(string ratio)
        {
            Inspection.Ratio = int.Parse(ratio);
            return this;
        }

        public Guid Id { get { return Inspection.Id; } }
        
    }
}
