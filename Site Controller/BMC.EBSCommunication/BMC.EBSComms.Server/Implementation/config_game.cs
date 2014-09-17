using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.EBSComms.Contracts.Dto;
using BMC.EBSComms.DataLayer.Dto;

namespace BMC.EBSComms.Server
{
    public partial class EBSCommServer
    {
        internal bool Parse_configurationInfo_getGame(s2sMessage target, object source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getGame");
            bool result = default(bool);
            getConfigurationGetGame s2s = source as getConfigurationGetGame;

            try
            {
                DLGameCollectionDto collection = _di.GetGames(target.p_body.p_configuration.propertyId, s2s.gameId.s2sStringId());
                result = this.Parse_configurationInfo_getGame(target, collection);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        internal bool Parse_configurationInfo_getGame(s2sMessage target, XElement source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getGame");
            bool result = default(bool);

            try
            {
                if (source != null)
                {
                    var elements = source.GetElements("Game");
                    if (elements != null)
                    {
                        foreach (var element in elements)
                        {
                            game_infoUpdate dto = new game_infoUpdate();
                            dto.gameId = element.GetElementValue("GameID");
                            result = this.AddObjectToInfoUpdateData<game_infoUpdate>(target, dto);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        internal bool Parse_configurationInfo_getGame(s2sMessage target, DLGameCollectionDto collection)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_configurationInfo_getGame");
            bool result = default(bool);

            try
            {
                configurationInfo ci = target.p_body.p_configuration.p_configurationInfo;
                if (collection != null)
                {
                    List<game> Games = new List<game>();
                    foreach (var dto in collection)
                    {
                        Games.Add(new game()
                        {
                            gameId = dto.GameID.ToStringSafe(),
                            gameName = dto.GameName,
                            gameActive = dto.IsActive,
                        });
                    }
                    ci.game = Games.ToArray();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }
}
