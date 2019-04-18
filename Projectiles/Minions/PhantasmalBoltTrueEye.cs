using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Minions
{
    public class PhantasmalBoltTrueEye : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_462";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phantasmal Bolt");
            Main.projFrames[projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.aiStyle = 1;
            aiType = ProjectileID.PhantasmalBolt;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.extraUpdates = 3;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }

        public override void AI()
        {
            int index = Dust.NewDust(projectile.Center, 0, 0, 229, 0.0f, 0.0f, 100, new Color(), 1f);
            Main.dust[index].noLight = true;
            Main.dust[index].noGravity = true;
            Main.dust[index].velocity = projectile.velocity;
            Main.dust[index].position -= Vector2.One * 4f;
            Main.dust[index].scale = 0.8f;
            int num1 = projectile.frameCounter + 1;
            projectile.frameCounter = num1;
            if (num1 >= 9)
            {
                projectile.frameCounter = 0;
                int num2 = projectile.frame + 1;
                projectile.frame = num2;
                if (num2 >= 5)
                    projectile.frame = 0;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("CurseoftheMoon"), 300);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.defense > 0)
                damage += target.defense / 2;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 128) * (1f - projectile.alpha / 255f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin2, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}