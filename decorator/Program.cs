#nullable disable
using System;

namespace Decorator
{
    public interface IHero
    {
        string GetDescription();
        int GetPower();
    }

    public class Warrior : IHero
    {
        public string GetDescription() => "Воїн:";
        public int GetPower() => 100;
    }

    public class Mage : IHero
    {
        public string GetDescription() => "Маг:";
        public int GetPower() => 80;
    }

    public class Palladin : IHero
    {
        public string GetDescription() => "Паладин:";
        public int GetPower() => 95;
    }

    public abstract class InventoryDecorator : IHero
    {
        protected IHero _hero;

        public InventoryDecorator(IHero hero)
        {
            _hero = hero;
        }

        public virtual string GetDescription() => _hero.GetDescription();
        public virtual int GetPower() => _hero.GetPower();
    }

    public class WeaponDecorator : InventoryDecorator
    {
        public WeaponDecorator(IHero hero) : base(hero) {}
        public override string GetDescription() => _hero.GetDescription() + " меч;";
        public override int GetPower() => _hero.GetPower() + 40;
    }

    public class ArmorDecorator : InventoryDecorator
    {
        public ArmorDecorator(IHero hero) : base(hero) {}
        public override string GetDescription() => _hero.GetDescription() + " броня;";
        public override int GetPower() => _hero.GetPower() + 30;
    }

    public class CloakDecorator : InventoryDecorator
    {
        public CloakDecorator(IHero hero) : base(hero) {}
        public override string GetDescription() => _hero.GetDescription() + " плащ;";
        public override int GetPower() => _hero.GetPower() + 15;
    }

    public class ArtifactDecorator : InventoryDecorator
    {
        public ArtifactDecorator(IHero hero) : base(hero) {}
        public override string GetDescription() => _hero.GetDescription() + " амулет;";
        public override int GetPower() => _hero.GetPower() + 20;
    }

    public class RingDecorator : InventoryDecorator
    {
        public RingDecorator(IHero hero) : base(hero) {}
        public override string GetDescription() => _hero.GetDescription() + " магічна каблучка;";
        public override int GetPower() => _hero.GetPower() + 25;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IHero warrior = new Warrior();
            warrior = new WeaponDecorator(warrior);
            warrior = new ArmorDecorator(warrior);
            warrior = new WeaponDecorator(warrior); 
            Console.WriteLine($"Персонаж - {warrior.GetDescription()} | Сила: {warrior.GetPower()}");

            IHero mage = new Mage();
            mage = new CloakDecorator(mage);
            mage = new ArtifactDecorator(mage);
            mage = new ArtifactDecorator(mage); 
            Console.WriteLine($"Персонаж - {mage.GetDescription()} | Сила: {mage.GetPower()}");

            IHero palladin = new Palladin();
            palladin = new ArmorDecorator(palladin);
            palladin = new RingDecorator(palladin);
            palladin = new WeaponDecorator(palladin);
            Console.WriteLine($"Персонаж - {palladin.GetDescription()} | Сила: {palladin.GetPower()}");

            Console.ReadKey();
        }
    }
}