using AutoMapper;
using AutoMapper.QueryableExtensions;
using Convenience.Data;
using Convenience.Migrations;
using Convenience.Models.DataModels;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static EntityFrameworkStudyWithConvenience.EntityFrameworkNestedObject;

namespace EntityFrameworkStudyWithConvenience {
    public class AutoMapperTest {
        private readonly ConvenienceContext _context;

        public AutoMapperTest(ConvenienceContext context) {
            _context = context;
        }

        public void AutoMapperTestExecution() {

            var res = _context.ShohinMaster
              .Include(sm => sm.ShiireMasters)
              .SelectMany(
                sm => sm.ShiireMasters!.DefaultIfEmpty(), 
                (sm,p) => new { sm, p });


            //AutoMapperを使わない場合は、
            //Selectの初期値記述が冗長
            var res1 = _context.ShohinMaster
                   .Include(sm => sm.ShiireMasters)
                   .SelectMany(
                        sm => sm.ShiireMasters!.DefaultIfEmpty(),
                        (sm,p) => new ProductShiireNameDTO {
                       Shohinid = sm.ShohinId,
                       ShohinName = sm.ShohinName,
                       ShohinTanka = sm.ShohinTanka,
                       ShohiZeiritsu = sm.ShohiZeiritsu,
                       ShohiZeiritsuEatIn = sm.ShohiZeiritsuEatIn,
                       ShiireSakiId = p != null ? p.ShiireSakiId : default,
                       ShiirePrdId = p != null ? p.ShiirePrdId : default,
                       ShiirePrdName = p != null ? p.ShiirePrdName : default,
                       ShiirePcsPerUnit = p != null ? p.ShiirePcsPerUnit : default,
                       ShiireUnit = p != null ? p.ShiireUnit : default,
                       ShireTanka = p != null ? p.ShireTanka : default,
                   }
                   );

            //AutoMapperを普通に使った場合、AutoMapperの記述が複雑に

            MapperConfiguration config2 = new MapperConfiguration(cfg => {
                cfg.CreateMap<ShohinShiirePair, ProductShiireNameDTO>()
                    .ForMember(dest => dest.Shohinid, opt => opt.MapFrom(src => src.Shohin.ShohinId))
                    .ForMember(dest => dest.ShohinName, opt => opt.MapFrom(src => src.Shohin.ShohinName))
                    .ForMember(dest => dest.ShohinTanka, opt => opt.MapFrom(src => src.Shohin.ShohinTanka))
                    .ForMember(dest => dest.ShohiZeiritsu, opt => opt.MapFrom(src => src.Shohin.ShohiZeiritsu))
                    .ForMember(dest => dest.ShohiZeiritsuEatIn, opt => opt.MapFrom(src => src.Shohin.ShohiZeiritsuEatIn))
                    .ForMember(dest => dest.ShiireSakiId,opt => opt.MapFrom(src => src.Shiire.ShiireSakiId))
                    .ForMember(dest => dest.ShiirePrdId, opt => opt.MapFrom(src => src.Shiire.ShiirePrdId))
                    .ForMember(dest => dest.ShiirePrdName, opt => opt.MapFrom(src => src.Shiire.ShiirePrdName))
                    .ForMember(dest => dest.ShiirePcsPerUnit, opt => opt.MapFrom(src => src.Shiire.ShiirePcsPerUnit))
                    .ForMember(dest => dest.ShiireUnit, opt => opt.MapFrom(src => src.Shiire.ShiireUnit))
                    .ForMember(dest => dest.ShireTanka, opt => opt.MapFrom(src => src.Shiire.ShireTanka))
                    ;
            });

            var res2 = _context.ShohinMaster
                .SelectMany(
                    sm => sm.ShiireMasters!.DefaultIfEmpty(),
                    (sm, p) => new ShohinShiirePair {
                    Shohin = sm,
                    Shiire = p != null ? p : new ShiireMaster()
                    })
                .ProjectTo<ProductShiireNameDTO>(config2)
                ;

            //AutoMapperのIncludeMembersを使った場合
            MapperConfiguration config3 = new MapperConfiguration(cfg => {
                cfg.CreateMap<ShohinShiirePair, ProductShiireNameDTO>()
                      .IncludeMembers(src => src.Shohin, src => src.Shiire);
                cfg.CreateMap<ShohinMaster, ProductShiireNameDTO>();
                cfg.CreateMap<ShiireMaster, ProductShiireNameDTO>();
            });

            var res3 = _context.ShohinMaster
                .SelectMany(
                    sm => sm.ShiireMasters!.DefaultIfEmpty(),
                    (sm, p) => new ShohinShiirePair {
                        Shohin = sm,
                        Shiire = p != null? p : new ShiireMaster()
                    })
                .ProjectTo<ProductShiireNameDTO>(config3)
                ;
        }

        //商品と仕入のペアクラス（中間オブジェクト）
        public class ShohinShiirePair {
            public ShohinMaster Shohin { get; set; } = new ShohinMaster();
            public ShiireMaster? Shiire { get; set; } = new ShiireMaster();
        }

        public class ProductShiireNameDTO {
            //商品マスタ系
            public string Shohinid { get; set; } = string.Empty;
            public string ShohinName { get; set; } = string.Empty;
            public decimal ShohinTanka { get; set; } = default;
            public decimal ShohiZeiritsu { get; set; } = default;
            public decimal ShohiZeiritsuEatIn { get; set; } = default;
            //仕入マスタ系
            public string? ShiireSakiId { get; set; } = string.Empty;
            public string? ShiirePrdId { get; set; } = string.Empty;
            public string? ShiirePrdName { get; set; } = string.Empty;
            public decimal ShiirePcsPerUnit { get; set; } = default;
            public string? ShiireUnit { get; set; } = string.Empty;
            public decimal ShireTanka { get; set; } = default;
        }
    }
}
