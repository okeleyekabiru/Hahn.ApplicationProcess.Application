﻿using Hahn.ApplicationProcess.February2021.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public Department Department { get; set; }
        public string CountryOfDepartment { get; set; }
        public string EMailAdressOfDepartment { get; set; }
        public bool Broken { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
     

    }
}
