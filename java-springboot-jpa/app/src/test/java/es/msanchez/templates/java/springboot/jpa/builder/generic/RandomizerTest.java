package es.msanchez.templates.java.springboot.jpa.builder.generic;

import es.msanchez.templates.java.springboot.jpa.entity.Person;
import org.assertj.core.api.BDDAssertions;
import org.assertj.core.api.SoftAssertions;
import org.testng.annotations.Test;

import java.util.ArrayList;
import java.util.List;

public class RandomizerTest {

    private Randomizer<Person> randomizer = new Randomizer<>();

    @Test
    public void testFillInstance() {
        // @GIVEN
        final Person bean = new Person();

        // @WHEN
        this.randomizer.fill(bean);

        // @THEN
        this.assertOne(bean);
    }

    private void assertOne(final Person bean) {
        BDDAssertions.assertThat(bean).isNotNull();
        SoftAssertions.assertSoftly(soft -> {
            soft.assertThat(bean.getName()).hasSize(50);
            soft.assertThat(bean.getAge()).isInstanceOf(Integer.class).isPositive();
            soft.assertThat(bean.getId()).isInstanceOf(Long.class).isPositive();
        });
    }

    @Test
    public void testFillList() {
        // @GIVEN
        final List<Person> list = this.prepareList();

        // @WHEN
        this.randomizer.fill(list);

        // @THEN
        list.forEach(this::assertOne);
    }

    private List<Person> prepareList() {
        final List<Person> list = new ArrayList<>();
        for (int i = 0; i < 5; i++) {
            list.add(new Person());
        }
        return list;
    }

}