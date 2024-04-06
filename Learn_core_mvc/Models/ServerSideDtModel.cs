using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class ServerSideDtModel
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpEmail { get; set; }
        public string EmpAddress { get; set; }
        public DateTime EmpJoinDate { get; set; }
        public string DisplayEmpJoinDate { get; set; }
        public decimal EmpSalary { get; set; }

        public List<ServerSideDtModel> GetData()
        {
            List<ServerSideDtModel> serverSideDtModels = new List<ServerSideDtModel>
            {
                new ServerSideDtModel{
                    EmpId=1,
                    EmpName="Aman",
                    EmpEmail="aman@gmail.com",
                    EmpAddress="101 aman nagar",
                    EmpJoinDate = Convert.ToDateTime("01/20/2022"),
                    EmpSalary = 20000
                },
                new ServerSideDtModel{
                    EmpId=2,
                    EmpName="Bman",
                    EmpEmail="bman@gmail.com",
                    EmpAddress="101 bman nagar",
                    EmpJoinDate = Convert.ToDateTime("02/21/2023"),
                    EmpSalary = 30000
                },
                new ServerSideDtModel{
                    EmpId=3,
                    EmpName="Chaman",
                    EmpEmail="chaman@gmail.com",
                    EmpAddress="101 chaman nagar",
                    EmpJoinDate = Convert.ToDateTime("03/22/2020"),
                    EmpSalary = 40000
                },
                new ServerSideDtModel{
                    EmpId=4,
                    EmpName="Daman",
                    EmpEmail="daman@gmail.com",
                    EmpAddress="101 daman nagar",
                    EmpJoinDate = Convert.ToDateTime("04/23/2021"),
                    EmpSalary = 26000
                },
                new ServerSideDtModel{
                    EmpId=5,
                    EmpName="Eman",
                    EmpEmail="eman@gmail.com",
                    EmpAddress="101 eman nagar",
                    EmpJoinDate = Convert.ToDateTime("05/24/2022"),
                    EmpSalary = 28000
                },
                new ServerSideDtModel{
                    EmpId=6,
                    EmpName="faman",
                    EmpEmail="faman@gmail.com",
                    EmpAddress="101 faman nagar",
                    EmpJoinDate = Convert.ToDateTime("06/25/2023"),
                    EmpSalary = 29000
                },
                new ServerSideDtModel{
                    EmpId=7,
                    EmpName="gaman",
                    EmpEmail="gaman@gmail.com",
                    EmpAddress="101 gaman nagar",
                    EmpJoinDate = Convert.ToDateTime("07/26/2018"),
                    EmpSalary = 22000
                },
                new ServerSideDtModel{
                    EmpId=8,
                    EmpName="haman",
                    EmpEmail="haman@gmail.com",
                    EmpAddress="101 haman nagar",
                    EmpJoinDate = Convert.ToDateTime("08/27/2019"),
                    EmpSalary = 32000
                },
                new ServerSideDtModel{
                    EmpId=9,
                    EmpName="iman",
                    EmpEmail="iman@gmail.com",
                    EmpAddress="101 iman nagar",
                    EmpJoinDate = Convert.ToDateTime("09/28/2012"),
                    EmpSalary = 50000
                },
                new ServerSideDtModel{
                    EmpId=10,
                    EmpName="Jaman",
                    EmpEmail="jaman@gmail.com",
                    EmpAddress="101 jaman nagar",
                    EmpJoinDate = Convert.ToDateTime("10/29/2013"),
                    EmpSalary = 45000
                }
            };

            return serverSideDtModels;
        }
    }
}
