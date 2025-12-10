using AoC2025.Solutions;
using Microsoft.AspNetCore.Mvc;

namespace AoC2025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionController(ILogger<SolutionController> logger) : ControllerBase
    {
        private ILogger _logger => logger;

        [HttpGet(Name = "GetSolution")]
        public string Get(int day, bool testInput = false, bool writeTestOutput = false)
        {
            switch (day)
            {
                case 1:
                    Day1 day1 = new(testInput);
                    return day1.GetSolution1() + "\n" + day1.GetSolution2();
                case 2:
                    Day2 day2 = new(testInput);
                    return day2.GetSolution1() + "\n" + day2.GetSolution2();
                case 3:
                    Day3 day3 = new(testInput);
                    return day3.GetSolution1() + "\n" + day3.GetSolution2();
                case 4:
                    Day4 day4 = new(testInput);
                    return day4.GetSolution1() + "\n" + day4.GetSolution2();
                case 5:
                    Day5 day5 = new(testInput);
                    return day5.GetSolution1() + "\n" + day5.GetSolution2();
                case 6:
                    Day6 day6 = new(testInput);
                    return day6.GetSolution1() + "\n" + day6.GetSolution2();
                case 7:
                    Day7 day7 = new(testInput);
                    return day7.GetSolution1(writeTestOutput) + "\n" + day7.GetSolution2(writeTestOutput);
                case 8:
                    Day8 day8 = new(testInput);
                    return day8.GetSolution1() + "\n" + day8.GetSolution2();
                default:
                    return "404";
            }
        }
    }
}
