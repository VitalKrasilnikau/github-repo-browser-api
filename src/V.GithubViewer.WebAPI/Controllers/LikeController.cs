using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using V.GithubViewer.DAL.Repository;
using V.GithubViewer.WebAPI.Model;

namespace V.GithubViewer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("*")]
    public class LikeController : Controller
    {
        private readonly IRedisRepository _redisRepository;

        public LikeController(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }

        [HttpGet("{key}")]
        public LikeData Get(string key) =>
            new LikeData { Repo = key, LikeCount = _redisRepository.Get(MapKey(key)) };

        [HttpPut("{key}")]
        public void Put(string key) => _redisRepository.Increment(MapKey(key));

        private static string MapKey(string key) => $"{key}_likes";
    }
}