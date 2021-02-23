using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadePrototype.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CascadeController : ControllerBase
    {
        private readonly ILogger<CascadeController> _logger;

        public CascadeController(ILogger<CascadeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll(int? cat1Id, int? cat2Id, int? cat3Id)
        {
            return Ok(new
            {
                Cat1 = ProcCat1(cat1Id),
                Cat2 = ProcCat2(cat1Id, cat2Id),
                Cat3 = ProcCat3(cat1Id, cat2Id, cat3Id),
            });
        }



        private List<Cat1> _cat1Collection = new ()
        {
            new () {Cat1Id = 1, Name = "Cat1"},
            new () {Cat1Id = 2, Name = "Cat2"},
            new () {Cat1Id = 3, Name = "Cat3"},
        };

        private List<Cat2> _cat2Collection = new ()
        {
            new () {Cat2Id = 1, Cat1Id = 1, Name = "Cat11"},
            new () {Cat2Id = 2, Cat1Id = 1, Name = "Cat21"},
            new () {Cat2Id = 3, Cat1Id = 1, Name = "Cat31"},
            new () {Cat2Id = 1, Cat1Id = 2, Name = "Cat12"},
        };

        private List<Cat3> _cat3Collection = new ()
        {
            new () {Cat3Id = 1, Cat2Id = 1, Name = "Cat11"},
            new () {Cat3Id = 2, Cat2Id = 1, Name = "Cat21"},
            new () {Cat3Id = 3, Cat2Id = 1, Name = "Cat31"},
            new () {Cat3Id = 1, Cat2Id = 2, Name = "Cat12"},
        };

        private IList<Cat1> ProcCat1(int? cat1Id)
        {
            return _cat1Collection
                .WhereIf(cat1Id.HasValue, x => x.Cat1Id == cat1Id)
                .ToList()
                ;
        }

        private IList<Cat2> ProcCat2(int? cat1Id, int? cat2Id)
        {
            return _cat2Collection
                .WhereIf(cat1Id.HasValue, x => x.Cat1Id == cat1Id)
                .WhereIf(cat2Id.HasValue, x => x.Cat2Id == cat2Id)
                .ToList()
                ;
        }

        private IList<Cat3> ProcCat3(int? cat1Id, int? cat2Id, int? cat3Id)
        {
            return _cat3Collection
                .WhereIf(cat2Id.HasValue, x => x.Cat2Id == cat2Id)
                .WhereIf(cat3Id.HasValue, x => x.Cat3Id == cat1Id)
                .ToList()
                ;
        }

        public class Cat1
        {
            public int Cat1Id { get; set; }
            public string Name { get; set; }
        }

        public class Cat2
        {
            public int Cat2Id { get; set; }
            public int Cat1Id { get; set; }
            public string Name { get; set; }
        }

        public class Cat3
        {
            public int Cat3Id { get; set; }
            public int Cat2Id { get; set; }
            public string Name { get; set; }
        }

    }
}
