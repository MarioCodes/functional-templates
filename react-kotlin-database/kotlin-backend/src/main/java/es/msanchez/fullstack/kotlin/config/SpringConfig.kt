package es.msanchez.fullstack.kotlin.config

import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.ComponentScan
import org.springframework.context.annotation.Configuration
import org.springframework.web.cors.CorsConfiguration
import org.springframework.web.cors.UrlBasedCorsConfigurationSource
import org.springframework.web.filter.CorsFilter
import java.util.*

@Configuration
@ComponentScan(basePackages = ["es.msanchez.fullstack.kotlin.**.*"])
class SpringConfig {

    @Bean
    fun corsFilter(): CorsFilter {
        val source = UrlBasedCorsConfigurationSource()
        val config = CorsConfiguration()
        config.allowCredentials = true
        config.allowedOrigins = Collections.singletonList("http://localhost:3000")
        config.allowedHeaders = listOf("Origin", "Content-Type", "Accept")
        config.allowedMethods = listOf("GET", "POST", "PUT", "OPTIONS", "DELETE")
        source.registerCorsConfiguration("/**", config)
        return CorsFilter(source)
    }

}