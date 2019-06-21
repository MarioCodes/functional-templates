package es.msanchez.templates.java.vertx.builder.implementation;

import es.msanchez.templates.java.vertx.entity.Person;
import org.assertj.core.api.BDDAssertions;
import org.testng.annotations.Test;

import java.util.List;

public class PersonBuilderTest {

    @Test
    public void testBuildOne() {
        // @GIVEN

        // @WHEN
        final Person person = PersonBuilder.getInstance().random().withAge(42).withName("mario").build();

        // @THEN
        BDDAssertions.assertThat(person).isNotNull();
        BDDAssertions.assertThat(person.getAge()).isEqualTo(42);
        BDDAssertions.assertThat(person.getName()).isEqualTo("mario");
    }

    @Test
    public void testBuildList() {
        // @GIVEN

        // @WHEN
        final List<Person> person = PersonBuilder.getInstance().random().withAge(42).withName("mario").buildList();

        // @THEN
        BDDAssertions.assertThat(person).isNotNull().hasSize(5);
        BDDAssertions.assertThat(person).extracting("age").hasSize(5).containsOnly(42);
        BDDAssertions.assertThat(person).extracting("name").hasSize(5).containsOnly("mario");
    }

}