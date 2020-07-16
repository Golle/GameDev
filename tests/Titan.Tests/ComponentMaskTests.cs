using NUnit.Framework;
using Titan.ECS.Components;

namespace Titan.Tests
{
    [TestFixture]
    internal class ComponentMaskTests
    {
        [Test]
        public void Contains_SingleComponentNoMatch_ReturnFalse()
        {
            var mask = new ComponentMask(0b_01);
            
            var result = mask.Contains(new ComponentMask(0b_10));

            Assert.That(result, Is.False);
        }
        
        [Test]
        public void Contains_SingleComponentMatch_ReturnTrue()
        {
            var mask = new ComponentMask(0b_1);
            
            var result = mask.Contains(new ComponentMask(0b_1));

            Assert.That(result, Is.True);
        }
        
        [Test]
        public void Contains_MultipleComponentsNoMatch_ReturnFalse()
        {
            var mask = new ComponentMask(0b_011);
            
            var result = mask.Contains(new ComponentMask(0b_100));

            Assert.That(result, Is.False);
        }

        [Test]
        public void Contains_MultipleComponentsSingleMatch_ReturnTrue()
        {
            var mask = new ComponentMask(0b_111);

            var result = mask.Contains(new ComponentMask(0b_010));

            Assert.That(result, Is.True);
        }
        
        [Test]  
        public void Contains_MultipleComponentsMultipleMatches_ReturnTrue()
        {
            var mask = new ComponentMask(0b_111);

            var result = mask.Contains(new ComponentMask(0b_110));

            Assert.That(result, Is.True);
        }


        [Test]
        public void IsSubsetOf_SingleComponentNoMatch_ReturnFalse()
        {
            var mask = new ComponentMask(0b_01);
            
            var result = mask.IsSubsetOf(new ComponentMask(0b_10));

            Assert.That(result, Is.False);
        }
        
        [Test]
        public void IsSubsetOf_SingleComponentMatch_ReturnTrue()
        {
            var mask = new ComponentMask(0b_1);
            
            var result = mask.IsSubsetOf(new ComponentMask(0b_1));

            Assert.That(result, Is.True);
        }
        
        [Test]
        public void IsSubsetOf_MultipleComponentNoMatch_ReturnFalse()
        {
            var mask = new ComponentMask(0b_011);
            
            var result = mask.IsSubsetOf(new ComponentMask(0b_100));

            Assert.That(result, Is.False);
        }
        
        [Test]
        public void IsSubsetOf_MultipleComponentSingleMatch_ReturnTrue()
        {
            var mask = new ComponentMask(0b_001);
            
            var result = mask.IsSubsetOf(new ComponentMask(0b_111));

            Assert.That(result, Is.True);
        }
        
        [Test]
        public void IsSubsetOf_MultipleComponentMultipleMatches_ReturnTrue()
        {
            var mask = new ComponentMask(0b_011);
            
            var result = mask.IsSubsetOf(new ComponentMask(0b_111));

            Assert.That(result, Is.True);
        }
    }
}
