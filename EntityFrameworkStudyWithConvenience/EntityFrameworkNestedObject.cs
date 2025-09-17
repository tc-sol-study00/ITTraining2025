using Convenience.Data;
using Convenience.Models.DataModels;
using Convenience.Models.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkStudyWithConvenience {
    internal class EntityFrameworkNestedObject {

        private readonly ConvenienceContext _context;
        public EntityFrameworkNestedObject(ConvenienceContext context) {
            this._context = context;
            this.ExecutionMethod();
        }
        private void ExecutionMethod() {
            var result = _context.ChumonJisseki
                .Where(cj => cj.ShiireSakiId == "A000000001" && cj.ChumonId == "20250801-001")
                .GroupJoin(
                    _context.ChumonJissekiMeisai,
                    cjgrp => new { cjgrp.ShiireSakiId, cjgrp.ChumonId },
                    cmgrp => new { cmgrp.ShiireSakiId, cmgrp.ChumonId },
                    (cjdata, cmdata) => new { cjdata, cmdatas = cmdata }
                )
                .SelectMany(
                    joineddata => joineddata.cmdatas.DefaultIfEmpty(),
                    (joineddata, nesteddata) => new { cjdata = joineddata.cjdata, cmdata = nesteddata }
                )
                .GroupJoin(
                    _context.ShiireMaster,
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
                        (joineddata, sms) => new { ChumonJisseki = joineddata.cjdata, ChumonJissekiMeisai = joineddata.cmdata, ShiireMasters = sms }
                    )
                .SelectMany(
                    joineddata => joineddata.ShiireMasters.DefaultIfEmpty(),
                    (joineddata, shiireMasters) => new { joineddata.ChumonJisseki, joineddata.ChumonJissekiMeisai, ShiireMaster = shiireMasters }
                )
                .GroupJoin(
                    _context.ShohinMaster,
                    connecteddata => connecteddata.ShiireMaster.ShohinId,
                    shohin => shohin.ShohinId,
                    (connecteddata, shohin) => new { connecteddata.ChumonJisseki, connecteddata.ChumonJissekiMeisai, connecteddata.ShiireMaster, ShohinMasters = shohin }
                )
                .SelectMany(
                    joineddata => joineddata.ShohinMasters.DefaultIfEmpty(),
                    (joineddata, shohinMasters) => new { joineddata.ChumonJisseki, joineddata.ChumonJissekiMeisai, joineddata.ShiireMaster, ShohinMaster = shohinMasters }
                );

            Console.WriteLine(result.ToQueryString());

            var res1 = _context.ChumonJisseki
            .Where(cj => cj.ShiireSakiId == "A000000001" && cj.ChumonId == "20250801-001")
            .Select(cj => new ChumonJisseki {
                ChumonId = cj.ChumonId,
                ChumonDate = cj.ChumonDate,
                ShiireSakiId = cj.ShiireSakiId,
                ChumonJissekiMeisais = _context.ChumonJissekiMeisai
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
                            .Select(shm => new ShiireMaster 
                                {
                                    ShiireSakiId = shm.ShiireSakiId,
                                    ShiirePrdId = shm.ShiirePrdId,
                                    ShohinId = shm.ShohinId,
                                    ShiirePrdName = shm.ShiirePrdName,
                                    ShiirePcsPerUnit = shm.ShiirePcsPerUnit,
                                    ShiireUnit = shm.ShiireUnit,
                                    ShireTanka = shm.ShireTanka,
                                    ShohinMaster =_context.ShohinMaster.FirstOrDefault(sho => sho.ShohinId == shm.ShohinId)
                                })
                            .FirstOrDefault()
                        })
                        .ToList()
            })
            .FirstOrDefault();





        }

    }
}
