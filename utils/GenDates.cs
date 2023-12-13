

public class GenDates 
{


    public List<DateTime> GetDays(DateTime? startDate = null, DateTime? endDate = null){
         DateTime currentDate = DateTime.Today;

        // If startDate is not provided, find the previous Sunday
        DateTime start = startDate ?? currentDate.AddDays(-(int)currentDate.DayOfWeek);

        // If endDate is not provided, find the coming Saturday
        DateTime end = endDate ?? currentDate.AddDays(6 - (int)currentDate.DayOfWeek);

        // Generate a list of dates from start to end
        List<DateTime> dateList = Enumerable.Range(0, (int)(end - start).TotalDays + 1)
            .Select(offset => start.AddDays(offset))
            .ToList();

        return dateList;
    }
}