using System;
using Kesmai.Server.Combat.Calculators;
using Kesmai.Server.Game;

namespace Kesmai.Server.Items
{
	public abstract partial class Weapon : ItemEntity, IWeapon, IArmored, IWieldable
	{
		/// <summary>
		/// Gets the attack bonus provided by this <see cref="IWeapon" /> for <see cref="MobileEntity" />.
		/// </summary>
		/// <remarks>
		/// Attack bonus provided by weapons is dependent on weapon skill, <see cref="PlayerEntity.Dexterity"/>, 
		/// and <see cref="BaseAttackBonus"/>.
		/// </remarks>
		public virtual double GetAttackBonus(MobileEntity attacker, MobileEntity defender)
			=> AttackBonus.GetAttackBonus(attacker, defender, this);
		
		/// <inheritdoc/>
		/// <remarks>
		/// Weapons only provide a blocking bonus against other weapons. Two-handed weapons
		/// should only provide a benefit if equipped in the right-hand.
		/// </remarks>
		public override int CalculateBlockingBonus(ItemEntity item)
		{
			if (item is IWeapon weapon && weapon.Flags.HasFlag(WeaponFlags.Projectile))
				return ProjectileProtection;

			return BaseArmorBonus;
		}
		
		/// <summary>
		/// Calculates the fumble chance as a percent.
		/// </summary>
		public override double CalculateFumbleChance(MobileEntity entity)
		{
			// Skill Level = 3 =>  ((3 + 1)^2) * 10 = 160 => 1 / 160;
			// Skill Level = 4 =>  ((4 + 1)^2) * 10 = 250 => 1 / 250;
			return 1 / (10 * Math.Pow(entity.GetSkillLevel(Skill) + 1, 2));
		}

		public virtual void OnWield(MobileEntity entity)
		{
		}
		
		public virtual void OnUnwield(MobileEntity entity)
		{
		}
	}
}