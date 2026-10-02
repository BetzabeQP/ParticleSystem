using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ParticleSystemExercise
{
    public class FireWorksParticleSystem: ParticleSystem
    {
        Color[] colors = new Color[]{ 
            Color.Fuchsia, 
            Color.Red,
            Color.Crimson,
            Color.CadetBlue,
            Color.Aqua,
            Color.HotPink,
            Color.LimeGreen,
            Color.BlanchedAlmond
        };
        Color color;
        public FireWorksParticleSystem(Game game, int maxExplosion):base(game, maxExplosion * 25)
        { }

        protected override void InitializeConstants()
        {
           textureFilename = "circle";
           minNumParticles = 20;
           maxNumParticles = 25;

           blendState = BlendState.Additive;
           DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(40, 200);
            var lifetime = RandomHelper.NextFloat(0.5f, 1.0f);
            var acceleration = -velocity/lifetime;
            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);
            var angluarVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);
            var scales = RandomHelper.NextFloat(4,6);
            p.Initialize(where, velocity,acceleration, color ,lifetime: lifetime, rotation, angluarVelocity,  scales);
        }

        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);
            float normalizedLifetime = particle.TimeSinceStart/particle.Lifetime;
            particle.Scale = .1f + .25f * normalizedLifetime;
        }

        public void PlaceFireWork(Vector2 where) 
        { 
            color = colors[RandomHelper.Next(colors.Length)];
            AddParticles(where);
        }

    }
}