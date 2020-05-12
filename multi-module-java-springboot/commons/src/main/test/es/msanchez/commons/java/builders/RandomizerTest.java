package es.msanchez.commons.java.builders;

import org.assertj.core.api.BDDAssertions;
import org.assertj.core.api.SoftAssertions;
import org.testng.annotations.Test;

import java.util.ArrayList;
import java.util.List;

public class RandomizerTest {

    private Randomizer<TestBean> randomizer = new Randomizer<>();

    @Test
    public void testFillInstance() {
        // @GIVEN
        final TestBean bean = new TestBean();

        // @WHEN
        this.randomizer.fill(bean);

        // @THEN
        this.assertOne(bean);
    }

    private void assertOne(final TestBean bean) {
        BDDAssertions.assertThat(bean).isNotNull();
        SoftAssertions.assertSoftly(soft -> {
            soft.assertThat(bean.getString()).hasSize(50);
            soft.assertThat(bean.getInteger()).isInstanceOf(Integer.class).isPositive();
            soft.assertThat(bean.getLongField()).isInstanceOf(Long.class).isPositive();
            soft.assertThat(bean.getBooleanField()).isInstanceOf(Boolean.class);
            soft.assertThat(bean.getDoubleField()).isInstanceOf(Double.class).isPositive();
            soft.assertThat(bean.getFloatField()).isInstanceOf(Float.class).isPositive();
        });
    }

    @Test
    public void testFillList() {
        // @GIVEN
        final List<TestBean> list = this.prepareList();

        // @WHEN
        this.randomizer.fill(list);

        // @THEN
        list.forEach(this::assertOne);
    }

    private List<TestBean> prepareList() {
        final List<TestBean> list = new ArrayList<>();
        for (int i = 0; i < 5; i++) {
            list.add(new TestBean());
        }
        return list;
    }

}