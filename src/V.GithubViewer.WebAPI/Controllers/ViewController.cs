using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using V.GithubViewer.DAL.Repository;
using V.GithubViewer.WebAPI.Model;

namespace V.GithubViewer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("*")]
    public class ViewController : Controller
    {
        private readonly IRedisRepository _redisRepository;

        public ViewController(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }

        [HttpGet("{key}")]
        public ViewData Get(string key) =>
            new ViewData { Repo = key, ViewCount = _redisRepository.Get(MapKey(key)) };

        [HttpPut("{key}")]
        public void Put(string key) => _redisRepository.Increment(MapKey(key));

        private static string MapKey(string key) => $"{key}_views";
    }
}