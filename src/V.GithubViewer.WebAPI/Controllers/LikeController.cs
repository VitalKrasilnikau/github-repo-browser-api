using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using V.GithubViewer.DAL.Repository;
using V.GithubViewer.WebAPI.Model;

namespace V.GithubViewer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class LikeController : Controller
    {
        private readonly RedisRepository _redisRepository;

        public LikeController(RedisRepository redisRepository)
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