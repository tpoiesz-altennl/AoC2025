using AoC2025.Solutions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AoC2025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeMeasuringController(ILogger<TimeMeasuringController> logger) : ControllerBase
    {
        private ILogger _logger => logger;

        [HttpGet(Name = "GetExecutionTime")]
        public string Get(int day, int solution, Int64 iterations = 1000)
        {
            switch (day)
            {
                case 1:
                    Day1 day1 = new(false);
                    Stopwatch watch = Stopwatch.StartNew();
                    for (int i = 0; i < iterations; ++i)
                    {
                        if (solution == 1)
                            day1.GetSolution1(); // discard return value
                        else if (solution == 2)
                            day1.GetSolution2();
                    }
                    watch.Stop();
                    return (watch.ElapsedMilliseconds / iterations).ToString();
                case 2:
                    Day2 day2 = new(false);
                    watch = Stopwatch.StartNew();
                    for (int i = 0; i < iterations; ++i)
                    {
                        if (solution == 1)
                            day2.GetSolution1();
                        else if (solution == 2)
                            day2.GetSolution2();
                    }
                    watch.Stop();
                    return (watch.ElapsedMilliseconds / iterations).ToString();
                case 3:
                    Day3 day3 = new(false);
                    watch = Stopwatch.StartNew();
                    for (int i = 0; i < iterations; ++i)
                    {
                        if (solution == 1)
                            day3.GetSolution1(); // discard return value
                        else if (solution == 2)
                            day3.GetSolution2();
                    }
                    watch.Stop();
                    return (watch.ElapsedMilliseconds / iterations).ToString();
                case 4:
                    Day4 day4 = new(false);
                    watch = Stopwatch.StartNew();
                    for (int i = 0; i < iterations; ++i)
                    {
                        if (solution == 1)
                            day4.GetSolution1(); // discard return value
                        else if (solution == 2)
                            day4.GetSolution2();
                    }
                    watch.Stop();
                    return (watch.ElapsedMilliseconds / iterations).ToString();
                case 5:
                    Day5 day5 = new(false);
                    watch = Stopwatch.StartNew();
                    for (int i = 0; i < iterations; ++i)
                    {
                        if (solution == 1)
                            day5.GetSolution1(); // discard return value
                        else if (solution == 2)
                            day5.GetSolution2();
                    }
                    watch.Stop();
                    return (watch.ElapsedMilliseconds / iterations).ToString();
                case 6:
                    Day6 day6 = new(false);
                    watch = Stopwatch.StartNew();
                    for (int i = 0; i < iterations; ++i)
                    {
                        if (solution == 1)
                            day6.GetSolution1(); // discard return value
                        else if (solution == 2)
                            day6.GetSolution2();
                    }
                    watch.Stop();
                    return (watch.ElapsedMilliseconds / iterations).ToString();
                case 7:
                    Day7 day7 = new(false);
                    watch = Stopwatch.StartNew();
                    for (int i = 0; i < iterations; ++i)
                    {
                        if (solution == 1)
                            day7.GetSolution1(); // discard return value
                        else if (solution == 2)
                            day7.GetSolution2();
                    }
                    watch.Stop();
                    return (watch.ElapsedMilliseconds / iterations).ToString();
                case 8:
                    Day8 day8 = new(false);
                    watch = Stopwatch.StartNew();
                    for (int i = 0; i < iterations; ++i)
                    {
                        if (solution == 1)
                            day8.GetSolution1(); // discard return value
                        else if (solution == 2)
                            day8.GetSolution2();
                    }
                    watch.Stop();
                    return (watch.ElapsedMilliseconds / iterations).ToString();
                case 11:
                    Day11 day11 = new(false);
                    watch = Stopwatch.StartNew();
                    for (int i = 0; i < iterations; ++i)
                    {
                        if (solution == 1)
                            day11.GetSolution1(); // discard return value
                        else if (solution == 2)
                            day11.GetSolution2();
                    }
                    watch.Stop();
                    return (watch.ElapsedMilliseconds / iterations).ToString();
                default:
                    return "404";
            }
        }
    }
}
