using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class NukeFishron : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nuke Fishron");
            Tooltip.SetDefault("'The highly weaponized remains of a defeated foe...'");
        }

        public override void SetDefaults()
        {
            item.damage = 300;
            item.ranged = true;
            item.width = 24;
            item.height = 24;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3f;
            item.UseSound = new LegacySoundStyle(2, 62);
            item.value = 50000;
            item.rare = 11;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("FishNuke");
            item.shootSpeed = 30f;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(Main.DiscoR, 51, 255 - (int)(Main.DiscoR * 0.4));
                }
            }
        }

        //make them hold it different
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0);
        }

        public override void AddRecipes()
        {
            if (Fargowiltas.Instance.FargosLoaded)
            {
                ModRecipe recipe = new ModRecipe(mod);

                recipe.AddIngredient(mod.ItemType("FishStick"), 5);
                recipe.AddIngredient(mod.ItemType("CyclonicFin"));
                recipe.AddIngredient(mod.ItemType("Sadism"));
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("EnergizerFish"));

                recipe.AddTile(mod, "CrucibleCosmosSheet");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}