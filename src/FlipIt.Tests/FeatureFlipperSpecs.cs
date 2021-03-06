using Machine.Specifications;
using Should.Fluent;

namespace FlipIt.Tests
{
    public abstract class behaves_like_feature_flipper_spec
    {
        protected static FeatureFlipper flipper;
        protected static TestFeatureSwitch featureSwitch;

        Establish context = () => flipper = new FeatureFlipper(new NullFeatureSettingsProvider());
    }

    public abstract class behaves_like_feature_flipper_spec_where_test_feature_is_on : behaves_like_feature_flipper_spec
    {
        Establish context = () => featureSwitch = new TestFeatureSwitch(true);
    }

    public abstract class behaves_like_feature_flipper_spec_where_test_feature_is_off : behaves_like_feature_flipper_spec
    {
        Establish context = () => featureSwitch = new TestFeatureSwitch(false);
    }

    public class when_checking_if_an_on_feature_is_on : behaves_like_feature_flipper_spec_where_test_feature_is_on
    {
        static bool result;
        Because it = () => result = flipper.IsOn(featureSwitch);
        It should_return_true = () => result.Should().Be.True();
        It should_call_feature_is_on = () => featureSwitch.TimesIsOnWasCalled.Should().Equal(1);
    }

    public class when_checking_if_an_off_feature_is_on : behaves_like_feature_flipper_spec_where_test_feature_is_off
    {
        static bool result;
        Because it = () => result = flipper.IsOn(featureSwitch);
        It should_return_false = () => result.Should().Be.False();
        It should_call_feature_is_on = () => featureSwitch.TimesIsOnWasCalled.Should().Equal(1);
    }

    public class when_doing_if_an_on_feature_is_on : behaves_like_feature_flipper_spec_where_test_feature_is_on
    {
        static bool didDo;
        Because of = () => flipper.DoIfOn(featureSwitch, () => { didDo = true; });
        It should_do_action = () => didDo.Should().Be.True();
    }

    public class when_doing_if_an_off_feature_is_on : behaves_like_feature_flipper_spec_where_test_feature_is_off
    {
        static bool didDo;
        Because of = () => flipper.DoIfOn(featureSwitch, () => { didDo = true; });
        It should_do_action = () => didDo.Should().Be.False();
    }
}