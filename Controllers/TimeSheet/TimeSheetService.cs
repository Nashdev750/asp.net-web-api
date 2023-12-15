using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Intacct.Models;




public class TimeSheetService 
{
    private readonly IntacctContext _context;

    public TimeSheetService(IntacctContext context)
        {
            _context = context;
        }

    public async Task<TimeSheet> CreateTimeSheet(string userId){
          try
            {
                GenDates gendates = new GenDates();
                var dates = gendates.GetDays();
                var currentSheet = _context.TimeSheets.Where(sheet=>sheet.Start == dates.First()).FirstOrDefault();
                if(currentSheet !=null) return currentSheet;
                var timeSheet = new TimeSheet{
                    Userid = int.Parse(userId),
                    Start = dates.First(),
                    End = dates.Last()
                };
                _context.TimeSheets.Add(timeSheet);
                var sheet = await _context.SaveChangesAsync();
                return timeSheet;
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
    }    

}


