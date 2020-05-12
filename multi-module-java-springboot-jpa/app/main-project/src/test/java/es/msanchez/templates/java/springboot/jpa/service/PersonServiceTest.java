package es.msanchez.templates.java.springboot.jpa.service;

import es.msanchez.templates.java.springboot.jpa.dao.PersonDao;
import es.msanchez.templates.java.springboot.jpa.entity.Person;
import org.assertj.core.api.BDDAssertions;
import org.mockito.BDDMockito;
import org.mockito.Mockito;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

import java.util.Optional;

public class PersonServiceTest {

    private PersonService service;

    private PersonDao daoMock;

    @BeforeMethod
    public void setUp() {
        this.daoMock = Mockito.mock(PersonDao.class);

        this.service = new PersonService(this.daoMock);
    }

    @Test
    public void testIsValidName() {
        // @GIVEN
        final String name = "mario";

        BDDMockito.given(this.daoMock.findOneByName(name))
                .willReturn(Optional.empty());

        // @WHEN
        final boolean isValid = this.service.isValid(name);

        // @THEN
        BDDMockito.verify(this.daoMock).findOneByName(name);
        BDDAssertions.assertThat(isValid).isTrue();
    }

    @Test
    public void testIsValidNameCaseFalse() {
        // @GIVEN
        final String name = "mario";

        BDDMockito.given(this.daoMock.findOneByName(name))
                .willReturn(Optional.of(new Person()));

        // @WHEN
        final boolean isValid = this.service.isValid(name);

        // @THEN
        BDDMockito.verify(this.daoMock).findOneByName(name);
        BDDAssertions.assertThat(isValid).isFalse();
    }

    @Test
    public void testSave() {
        // @GIVEN
        final Person person = new Person();
        person.setName("mario");

        // @WHEN
        this.service.save(person);

        // @THEN
        BDDMockito.verify(this.daoMock).save(person);
    }
}