using AutoMapper;
using AutoMapper.QueryableExtensions;
using Convenience.Data;
using Convenience.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace EntityFrameworkStudyWithConvenience {
    internal class EntityFrameworkNestedObject {

        private readonly ConvenienceContext _context;
        public EntityFrameworkNestedObject(ConvenienceContext context) {
            this._context = context;
            this.ExecutionMethod();
        }
        private void ExecutionMethod() {

            /*
             * 複数エンティティの外部結合
             * １）注文実績
             * ２）注文実績明細
             * ３）仕入マスタ
             * ４）商品マスタ
             */

            var result = _context.ChumonJisseki //注文実績
                .Where(cj => cj.ShiireSakiId == "A000000001" && cj.ChumonId == "20250801-001")
                .GroupJoin(
                    _context.ChumonJissekiMeisai,   //注文実績明細
                    cjgrp => new { cjgrp.ShiireSakiId, cjgrp.ChumonId },    //結合キー（結合元：注文実績)
                    cmgrp => new { cmgrp.ShiireSakiId, cmgrp.ChumonId },    //結合キー（結合先：注文実績明細)
                    (cjdata, cmdata) => new { cjdata, cmdatas = cmdata }    //cjdata１件につき、cmddata2が複数にネストした状態になる
                )
                .SelectMany(            //ネスト状態をフラットにするのが、SelectMany
                    joineddata => joineddata.cmdatas.DefaultIfEmpty(),  //結合先が0件だったら、nullにする
                    (joineddata, nesteddata) => new { cjdata = joineddata.cjdata, cmdata = nesteddata } //cjdataとcmddataをフラットに配置
                )
                .GroupJoin(
                    _context.ShiireMaster,          //仕入マスタ
                        joineddata => new {
                            joineddata.cmdata.ShiireSakiId,
                            joineddata.cmdata.ShiirePrdId,
                            joineddata.cmdata.ShohinId
                        },
                        smgrp => new {
                            smgrp.ShiireSakiId,
                            smgrp.ShiirePrdId,
                            smgrp.ShohinId
                        },
                        (joineddata, sms) => 
                            new { ChumonJisseki = joineddata.cjdata, ChumonJissekiMeisai = joineddata.cmdata, ShiireMasters = sms }
                    )
                .SelectMany(                        
                    joineddata => joineddata.ShiireMasters.DefaultIfEmpty(),
                    (joineddata, shiireMasters) => 
                        new { joineddata.ChumonJisseki, joineddata.ChumonJissekiMeisai, ShiireMaster = shiireMasters }
                )
                .GroupJoin(
                    _context.ShohinMaster,          //商品マスタ
                    connecteddata => connecteddata.ShiireMaster.ShohinId,
                    shohin => shohin.ShohinId,
                    (connecteddata, shohin) => 
                        new { connecteddata.ChumonJisseki, connecteddata.ChumonJissekiMeisai, connecteddata.ShiireMaster, ShohinMasters = shohin }
                )
                .SelectMany(
                    joineddata => joineddata.ShohinMasters.DefaultIfEmpty(),
                    (joineddata, shohinMasters) => 
                        new { joineddata.ChumonJisseki, joineddata.ChumonJissekiMeisai, joineddata.ShiireMaster, ShohinMaster = shohinMasters }
                );

            Console.WriteLine(result.ToQueryString());

            /*
             * フラットじゃなく、nestで作る方法
             */
            var res1 = _context.ChumonJisseki
            .Where(cj => cj.ShiireSakiId == "A000000001" && cj.ChumonId == "20250801-001")
            .Select(cj => new ChumonJisseki {
                ChumonId = cj.ChumonId,
                ChumonDate = cj.ChumonDate,
                ShiireSakiId = cj.ShiireSakiId,
                ChumonJissekiMeisais = _context.ChumonJissekiMeisai //注文実績明細のネスト
                    .Where(cjm => cjm.ShiireSakiId == cj.ShiireSakiId && cjm.ChumonId == cj.ChumonId)
                    .Select(cjm => new ChumonJissekiMeisai {
                        ShiireSakiId = cjm.ShiireSakiId,
                        ChumonId = cjm.ChumonId,
                        ShiirePrdId = cjm.ShiirePrdId,
                        ShohinId = cjm.ShohinId,
                        // ネストして仕入マスタをセット
                        ShiireMaster = _context.ShiireMaster
                            .Where(sm =>
                                sm.ShiireSakiId == cjm.ShiireSakiId &&
                                sm.ShiirePrdId == cjm.ShiirePrdId &&
                                sm.ShohinId == cjm.ShohinId)
                            .Select(shm => new ShiireMaster {
                                ShiireSakiId = shm.ShiireSakiId,
                                ShiirePrdId = shm.ShiirePrdId,
                                ShohinId = shm.ShohinId,
                                ShiirePrdName = shm.ShiirePrdName,
                                ShiirePcsPerUnit = shm.ShiirePcsPerUnit,
                                ShiireUnit = shm.ShiireUnit,
                                ShireTanka = shm.ShireTanka,
                                ShohinMaster = _context.ShohinMaster.FirstOrDefault(sho => sho.ShohinId == shm.ShohinId)
                            })
                            .FirstOrDefault()
                    }).ToList()
            })
            .FirstOrDefault();

            /*
             * nest結合は、Includeのほうが短くかける
             */

            var data = _context.ChumonJissekiMeisai
                .Where(cjm => cjm.ShiireSakiId == "A000000001" && cjm.ChumonId == "20250801-001")
                .Include(cjm => cjm.ChumonJisseki)
                .ThenInclude(cj => cj.ShiireSakiMaster)
                .Include(cjm => cjm.ShiireMaster)
                .ThenInclude(sm => sm!.ShohinMaster)
                .ToList()
                ;

            /*
             * automapperと連携した例
             */
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ChumonJissekiMeisai, ChumonJissekiDTO>();
            });

            var mapper = config.CreateMapper();

            List<ChumonJissekiDTO> chumonjissekiDTO =
                mapper.Map<List<ChumonJissekiDTO>>(data);

            //
            // ProjectTo
            // LinqとAutoMapper合せ技


            var config2 = new MapperConfiguration(cfg => {
                cfg.CreateMap<ChumonJissekiMeisai, ChumonJissekiDTO>()
                .ForMember(dest => dest.ShiireSakiKaisya, opt => opt.MapFrom(src => src.ChumonJisseki.ShiireSakiMaster.ShiireSakiKaisya))
                .ForMember(dest => dest.ShiirePrdName, opt => opt.MapFrom(src => src.ShiireMaster.ShiirePrdName))
                .ForMember(dest => dest.ShohinName, opt => opt.MapFrom(src => src.ShiireMaster.ShohinMaster.ShohinName));
            });

            List<ChumonJissekiDTO> data2 = _context.ChumonJissekiMeisai
                .Where(cjm => cjm.ShiireSakiId == "A000000001" && cjm.ChumonId == "20250801-001")
                .ProjectTo<ChumonJissekiDTO>(config2)               //Automapperは転送元と転送先のクラス定義が前提
                .ToList()
                ;

            //AutoMapper使わないと・・・

            List<ChumonJissekiDTO> data3 = _context.ChumonJissekiMeisai
                .Where(cjm => cjm.ShiireSakiId == "A000000001" && cjm.ChumonId == "20250801-001")
                .Include(x => x.ChumonJisseki)
                .ThenInclude(x => x.ShiireSakiMaster)
                .Include(x => x.ShiireMaster)
                .ThenInclude(x => x.ShohinMaster)
                .Select(d =>
                    new ChumonJissekiDTO {  //投影(Projection)という
                        ChumonId = d.ChumonId,
                        ChumonDate = d.ChumonJisseki.ChumonDate,
                        ShiireSakiId = d.ShiireSakiId,
                        ShiireSakiKaisya = d.ChumonJisseki.ShiireSakiMaster.ShiireSakiKaisya,
                        ShiirePrdId = d.ShiirePrdId,
                        ShiirePrdName = d.ShiireMaster.ShiirePrdName,
                        ShohinId = d.ShohinId,
                        ShohinName = d.ShiireMaster.ShohinMaster.ShohinName,
                        ChumonSu = d.ChumonSu
                    }
                )
                .ToList()
                ;
            
            /*
             * 外部結合（商品軸に倉庫在庫を集計した例
             */

            var listAllCast = _context.ShohinMaster
                .GroupJoin(_context.ShiireMaster,
                    shohin => shohin.ShohinId,
                    shiireM => shiireM.ShohinId,
                    (shohin, shiireM) => new { shohin, shiireM }
                    )
                .SelectMany(x => x.shiireM.DefaultIfEmpty(), (x, shiireM) => new { shohin = x.shohin, shiireM })
                .GroupJoin(_context.ShiireJisseki,
                    x => new { x.shiireM.ShiireSakiId, x.shiireM.ShiirePrdId, x.shiireM.ShohinId },
                    shiire => new { shiire.ShiireSakiId, shiire.ShiirePrdId, shiire.ShohinId },
                    (x, shiireGroup) => new { x.shohin, x.shiireM, shiireGroup }
                )
                .SelectMany(x => x.shiireGroup.DefaultIfEmpty(), (x, shiire) => new { x.shohin, x.shiireM, shiire })
                .GroupJoin(_context.ChumonJissekiMeisai,
                    x => new { x.shiireM.ShiireSakiId, x.shiireM.ShiirePrdId, x.shiireM.ShohinId },
                    chumon => new { chumon.ShiireSakiId, chumon.ShiirePrdId, chumon.ShohinId },
                    (x, chumonGroup) => new { x.shohin, x.shiireM, x.shiire, chumonGroup }
                )
                .SelectMany(x => x.chumonGroup.DefaultIfEmpty(), (x, chumon) => new { x.shohin, x.shiireM, x.shiire, chumon })
                .GroupJoin(_context.SokoZaiko,
                     x => new { x.shiireM.ShiireSakiId, x.shiireM.ShiirePrdId, x.shiireM.ShohinId },
                     soko => new { soko.ShiireSakiId, soko.ShiirePrdId, soko.ShohinId },
                    (x, sokoGroup) => new { x.shohin, x.shiireM, x.shiire, x.chumon, sokoGroup }
                )
                .SelectMany(x => x.sokoGroup.DefaultIfEmpty(), (x, soko) => new { x.shohin, x.shiireM, x.shiire, x.chumon, soko })
                .GroupBy(x => x.shohin.ShohinId)
                .Select(g => new SummaryDTO {
                    ShohinId = g.Key,
                    ShohinName = g.Min(x => x.shohin.ShohinName),
                    TotalChumonSu = g.Sum(x => x.chumon != null ? (int?)x.chumon.ChumonSu : 0) ?? 0,
                    TotalNonyuSu = g.Sum(x => x.shiire != null ? (int?)x.shiire.NonyuSu : 0) ?? 0,
                    TotalSokoZaikoSu = g.Sum(x => x.soko != null ? (int?)x.soko.SokoZaikoSu : 0) ?? 0
                })
                ;
        }

        private class SummaryDTO {
            public required string ShohinId {get;init;}
            public required string ShohinName { get; init; }
            public required decimal TotalChumonSu {get;init;}
            public required decimal TotalNonyuSu {get;init;}
            public required decimal TotalSokoZaikoSu { get; init; }
        }
        internal class ChumonJissekiDTO {
            public string ChumonId { get; set; }
            public DateOnly ChumonDate { get; set; }
            public string ShiireSakiId { get; set; }
            public string ShiireSakiKaisya { get; set; }
            public string ShiirePrdId { get; set; }
            public string ShiirePrdName { get; set; }
            public string ShohinId { get; set; }
            public string ShohinName { get; set; }
            public decimal ChumonSu { get; set; }
        }

    }
}
