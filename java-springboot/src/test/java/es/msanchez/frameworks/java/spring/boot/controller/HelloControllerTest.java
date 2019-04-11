package es.msanchez.frameworks.java.spring.boot.controller;

import static org.assertj.core.api.Assertions.assertThat;

import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.junit.MockitoJUnitRunner;
import org.springframework.http.HttpStatus;
import org.springframework.mock.web.MockHttpServletResponse;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;
import org.springframework.test.web.servlet.setup.MockMvcBuilders;

@RunWith(MockitoJUnitRunner.class)
public class HelloControllerTest {

  private MockMvc mvcMock;

  @InjectMocks
  private HelloController controller;

  @Before
  public void setUp() {
    this.mvcMock = MockMvcBuilders.standaloneSetup(this.controller).build();
  }

  @Test
  public void givenMockMvcWhenGetThenHttpStatusOk() throws Exception {
    // Given

    // When
    final MockHttpServletResponse response = this.mvcMock.perform(
        MockMvcRequestBuilders.get("/")).andReturn().getResponse();

    // Then
    assertThat(response.getStatus()).isEqualTo(HttpStatus.OK.value());
  }

  @Test
  public void givenMockMvcWhenGetThenExpectedStringResponse() throws Exception {
    // Given

    // When
    final MockHttpServletResponse response = this.mvcMock.perform(
        MockMvcRequestBuilders.get("/")).andReturn().getResponse();

    // Then
    assertThat(response.getContentAsString()).isEqualTo("Greetings from Spring Boot!");
  }

}
