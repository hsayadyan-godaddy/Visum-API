using Product.DAL.Simulation.Abstraction;
using Product.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Product.DAL.Simulation
{
    internal class SimulatedWellsAndProjectsRepository : ISimulatedWellsAndProjectsRepository
    {
        #region members

        private readonly Dictionary<string, List<WellboreInfo>> _simulatedData = new Dictionary<string, List<WellboreInfo>>();
        private readonly object _locker = new object();

        private readonly string[] _nameOne = new string[]
        {
            "Siyana",
            "Amber",
            "Clay",
            "Rio",
            "Samera",
            "Kourtney",
            "Amelia",
            "Kaitlan",
            "Raife",
            "Madeeha",
            "Kaci",
            "Cohan",
            "Ahmet",
            "Mikael",
            "Kieran",
            "Marianne",
            "Izabelle",
            "Cameron",
            "Arman",
            "Susan"
        };

        private readonly string[] _nameTwo = new string[]
        {
              "Hills",
              "Almond",
              "Mcnamara",
              "Dawson",
              "Campos",
              "Krause",
              "Mae",
              "Caldwell",
              "Thorpe",
              "Keenan",
              "Coates",
              "Thatcher",
              "Andrew",
              "Beck",
              "Lindsay",
              "Bridges",
              "Harding",
              "Acevedo",
              "Mcfadden",
              "Begum"};

        private readonly int[] _someNumbers = new int[] { 1001, 1200, 370, 75, 90, 1700, 2500, 1050, 5000, 3000, };

        #endregion members

        public SimulatedWellsAndProjectsRepository()
        {
            CreateRandomData();
        }

        public Task<List<string>> GetWellNamesToCompleteAsync(string searchFilter, string projectId, int maxResults)
        {
            List<WellboreInfo> tmp = null;
            var srchInfo = CreateSearchInfo(searchFilter);
            var searchLine = srchInfo.searchLine;
            var searchVector = srchInfo.searchVector;

            return Task.Run(() =>
            {
                if (string.IsNullOrEmpty(searchLine))
                {
                    return null;
                }

                lock (_locker)
                {
                    if (string.IsNullOrEmpty(projectId))
                    {
                        tmp = _simulatedData.SelectMany(x => x.Value).ToList();
                    }
                    else
                    {
                        if (_simulatedData.ContainsKey(projectId))
                        {
                            tmp = _simulatedData[projectId].ToList();
                        }
                    }
                }

                var ret = tmp.Where(x => x.Wellbore.Name.StartsWith(searchLine, StringComparison.OrdinalIgnoreCase)
                                      || x.Wellbore.Name.Contains(searchLine, StringComparison.OrdinalIgnoreCase)
                                      || (searchVector?.Length > 1 && searchVector
                                            .Any(z => x.Wellbore.Name.Contains(z, StringComparison.OrdinalIgnoreCase))))
                             .Select(x => x.Wellbore.Name)
                             .Distinct()
                             .OrderBy(x => x)
                             .Take(maxResults)
                             .ToList();

                return ret;
            });
        }

        public Task<PagedResult<WellboreInfo>> GetWellsAsync(string searchFilter, int resultsPerPage, int pageIndex, string projectId)
        {
            var srchInfo = CreateSearchInfo(searchFilter);
            var searchLine = srchInfo.searchLine;
            var searchVector = srchInfo.searchVector;

            return Task.Run(() =>
            {
                var ret = new PagedResult<WellboreInfo>();

                List<WellboreInfo> tmp = null;

                lock (_locker)
                {
                    if (string.IsNullOrEmpty(projectId))
                    {
                        tmp = _simulatedData.SelectMany(x => x.Value).ToList();
                    }
                    else
                    {
                        if (_simulatedData.ContainsKey(projectId))
                        {
                            tmp = _simulatedData[projectId].ToList();
                        }
                    }
                }

                var selection = tmp;

                if (!string.IsNullOrEmpty(searchLine) && tmp != null)
                {

                    var selectionStep = tmp.Where(x => x.Wellbore.Name.StartsWith(searchLine, StringComparison.OrdinalIgnoreCase)
                                                    || x.Wellbore.Name.Contains(searchLine, StringComparison.OrdinalIgnoreCase)
                                                    || (searchVector?.Length > 1 && searchVector
                                                            .Any(z => x.Wellbore.Name.Contains(z, StringComparison.OrdinalIgnoreCase))));

                    if (selectionStep.Count() == 0)
                    {
                        selectionStep = tmp.Where(x => x.Project.Name.StartsWith(searchLine, StringComparison.OrdinalIgnoreCase)
                                                    || x.Project.PadName.StartsWith(searchLine, StringComparison.OrdinalIgnoreCase)
                                                    || x.Project.Field.StartsWith(searchLine, StringComparison.OrdinalIgnoreCase)
                                                    || x.Wellbore.WellType.StartsWith(searchLine, StringComparison.OrdinalIgnoreCase)
                                                    || x.Project.Name.Contains(searchLine, StringComparison.OrdinalIgnoreCase)
                                                    || x.Project.PadName.Contains(searchLine, StringComparison.OrdinalIgnoreCase)
                                                    || x.Project.Field.Contains(searchLine, StringComparison.OrdinalIgnoreCase)
                                                    || x.Wellbore.WellType.Contains(searchLine, StringComparison.OrdinalIgnoreCase));
                    }


                    if (selectionStep.Count() > 0 && searchVector?.Length > 1)
                    {
                        selectionStep = selectionStep.Where(x => searchVector.Any(z => x.Project.Name.Contains(z, StringComparison.OrdinalIgnoreCase))
                                                              || searchVector.Any(z => x.Project.PadName.Contains(z, StringComparison.OrdinalIgnoreCase))
                                                              || searchVector.Any(z => x.Project.Field.Contains(z, StringComparison.OrdinalIgnoreCase))
                                                              || searchVector.Any(z => x.Wellbore.WellType.Contains(z, StringComparison.OrdinalIgnoreCase)));

                    };


                    selection = selectionStep.OrderBy(x => x.Wellbore.Name)
                                             .ToList();
                }

                if (selection?.Count > resultsPerPage)
                {
                    var pages = Math.Ceiling(selection.Count / (double)resultsPerPage);

                    ret.TotalPages = (int)pages;
                    ret.Result = selection.OrderBy(x => x.Wellbore.Name)
                                          .Skip(pageIndex * resultsPerPage)
                                          .Take(resultsPerPage)
                                          .ToList();
                    ret.CurrentPageIndex = pageIndex;
                }
                else
                {
                    ret.Result = selection;
                }

                return ret;
            });
        }

        private void CreateRandomData()
        {
            var rand = new Random(DateTime.Now.Second);
            rand.Next();

            for (int i = 0; i < 10000; i++)
            {
                var projName = i == 0 ? "Current" : $"{RandomName(rand, true)}-{RandomName(rand, false)}";

                if (!_simulatedData.ContainsKey(projName))
                {
                    var fieldName = i == 0 ? "Default Field" : RandomName(rand, true);
                    var padName = i == 0 ? "Default Pad" : RandomName(rand, false);

                    var projInfo = new ProjectBase
                    {
                        ProjectId = $"0000-{projName}-0001",
                        Field = fieldName,
                        Name = projName,
                        PadName = padName
                    };

                    var tmp = new List<WellboreInfo>();
                    _simulatedData.Add(projName, tmp);

                    for (int k = 0; k < 100; k++)
                    {
                        var wellName = RandomName(rand, false, true);

                        var wellInfo = new WellboreBase
                        {
                            Name = wellName,
                            WellId = $"ABCD-{wellName}-0000",
                            WellType = "unknown"
                        };
                        tmp.Add(new WellboreInfo
                        {
                            Project = projInfo,
                            Wellbore = wellInfo
                        });
                    }
                }
            }
        }

        private string RandomName(Random rand, bool firstNames, bool useNumbers = false)
        {
            var name = firstNames ? _nameOne[rand.Next(0, _nameOne.Length - 1)]
                                       : _nameTwo[rand.Next(0, _nameTwo.Length - 1)];

            if (useNumbers)
            {
                var num = _someNumbers[rand.Next(0, _someNumbers.Length - 1)];
                name = $"{name}-{num}";
            }

            return name;
        }

        private (string searchLine, string[] searchVector) CreateSearchInfo(string searchFilter)
        {
            var searchLine = searchFilter?.Trim();

            ///
            /// replace many spaces on one
            ///
            if (!string.IsNullOrEmpty(searchLine))
            {
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);
                searchLine = regex.Replace(searchLine, " ");
            }
            else
            {
                searchLine = string.Empty;
            }

            var searchVector = string.IsNullOrEmpty(searchLine) ? null : searchLine.Split(' ');

            return (searchLine, searchVector);

        }

    }
}
