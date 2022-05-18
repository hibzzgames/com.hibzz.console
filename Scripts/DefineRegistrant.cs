/* Description: Registers required defines with the Define Manager
 * Author:		Hibnu Hishath (sliptrixx)
 * Date:		18 May, 2022
 */

#if ENABLE_DEFINE_MANAGER
using Hibzz.DefineManager;

namespace Hibzz.Console
{
	internal class DefineRegistrant
	{
		[RegisterDefine]
		static DefineRegistrationData RegisterPrebuiltCommands()
        {
			DefineRegistrationData data = new DefineRegistrationData();

			data.Define = "ENABLE_HIBZZ_CONSOLE_PREBUILT_COMMANDS";
			data.DisplayName = "Pre-built Commands";
			data.Category = "Hibzz.Console";
			data.Description = "When enabled, this package provides optional pre-built " +
                "scripts that can give quick access to certain useful commands.";
			data.EnableByDefault = true;

			return data;
        }

		[RegisterDefine]
		static DefineRegistrationData RegisterCacheDictionary()
        {
			DefineRegistrationData data = new DefineRegistrationData();

			data.Define = "ENABLE_HIBZZ_CONSOLE_CACHE_DICTIONARY";
			data.DisplayName = "Cache Dictionary";
			data.Category = "Hibzz.Console";
			data.Description = "Cache Dictionary is a tool provided by the console that let's " +
                "commands cache and store data to communicate with each other";
			data.EnableByDefault = false;

			return data;
		}
	}
}
#endif