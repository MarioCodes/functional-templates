package es.codes.mario.template.di

import dagger.Module
import dagger.Provides
import es.codes.mario.template.charts.AndroidPlotChart
import es.codes.mario.template.utils.DateUtils
import es.codes.mario.template.viewmodel.EntryViewModel

@Module
class AppModule {

    // classes that are going to be injected go here.

    @Provides
    fun provideFileUtils(): EntryViewModel {
        return EntryViewModel()
    }

    @Provides
    fun provideDateUtils(): DateUtils {
        return DateUtils()
    }

    @Provides
    fun provideAndroidPlotChart(): AndroidPlotChart {
        return AndroidPlotChart()
    }

}