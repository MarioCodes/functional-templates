package es.codes.mario.prueba.config;

import org.springframework.cache.CacheManager;
import org.springframework.cache.annotation.EnableCaching;
import org.springframework.cache.ehcache.EhCacheCacheManager;
import org.springframework.cache.ehcache.EhCacheManagerFactoryBean;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;
import org.springframework.core.io.ClassPathResource;

import java.util.Objects;

@Configuration
@EnableCaching
@ComponentScan(basePackages = {
        "es.codes.mario.prueba.**"
})
public class SpringConfig {

    // EHCache configuration. All caches and variables are configured inside 'ehcache.xml'.

    @Bean
    public CacheManager cacheManager() {
        return new EhCacheCacheManager(Objects.requireNonNull(this.ehCacheManager().getObject()));
    }

    @Bean
    public EhCacheManagerFactoryBean ehCacheManager() {
        final EhCacheManagerFactoryBean factory = new EhCacheManagerFactoryBean();
        factory.setConfigLocation(new ClassPathResource("ehcache.xml"));
        factory.setShared(true);
        return factory;
    }

}
