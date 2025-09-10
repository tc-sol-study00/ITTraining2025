using AutoMapper;
using UseAutoMapper.DTOs;

namespace UseAutoMapper.Services {
    internal class AutoMappers {
        public List<Education> AutoMapperMethod() {

            List<Education> src = new List<Education>()
            {   new Education(){ ClassCode = "A",SeitoNo="01",KokugoScore=1,SuugakuScore=2,RikaScore=3 },
                new Education(){ ClassCode = "A",SeitoNo="02",KokugoScore=11,SuugakuScore=12,RikaScore=13},
                new Education(){ ClassCode = "B",SeitoNo="01",KokugoScore=21,SuugakuScore=22,RikaScore=23},
                new Education(){ ClassCode = "B",SeitoNo="02",KokugoScore=31,SuugakuScore=23,RikaScore=33},
            };


            List<Education> dest = src; //シャローコピー

            src[0].ClassCode = "C";

            //destの内容は？結果＝☓


            //自分で書くと
            List<Education> educations = new List<Education>();
            foreach (Education aEducation in src) {
                Education createdAEducation = new Education() {
                    ClassCode = aEducation.ClassCode,
                    SeitoNo = aEducation.SeitoNo,
                    KokugoScore = aEducation.KokugoScore,
                    SuugakuScore = aEducation.SuugakuScore,
                    RikaScore = aEducation.RikaScore
                };
                educations.Add(createdAEducation);
            }




            //ディープコピー
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Education, Education>();  //送り元・送り先
                cfg.CreateMap<Education, EducationTotal>()
                 .ForMember(dest => dest.TotalScore, opt => opt.MapFrom((src, dest) => src.KokugoScore + src.SuugakuScore + src.RikaScore));
            });

            Mapper mapper = new Mapper(mapperConfig);

            List<Education> dest2 = mapper.Map<List<Education>>(src);

            List<EducationTotal> educationTotal = mapper.Map<List<EducationTotal>>(src);

            return dest2;

        }
    }
}
