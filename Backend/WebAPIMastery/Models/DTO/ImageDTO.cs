﻿namespace WebAPIMastery.Models.DTO
{
    public class ImageDTO
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; } 

    }
}
