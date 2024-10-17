package es.codes.mario.template.viewmodel

import android.content.Context
import android.view.View
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import es.codes.mario.template.MainActivity
import es.codes.mario.template.data.Entry
import es.codes.mario.template.di.DaggerAppComponent
import es.codes.mario.template.utils.DateUtils
import java.io.File
import java.io.FileNotFoundException
import javax.inject.Inject

class EntryViewModel: ViewModel() {

    @Inject
    lateinit var dateUtils: DateUtils

    val entries = MutableLiveData<MutableList<Entry>>()

    init {
        DaggerAppComponent.create().inject(this)
    }

    companion object {
        val context = MainActivity.instance
    }


    fun add(entry: Entry) {
        // do something
    }

    fun delete(entry: Entry) {
        // do something
    }

    fun resetFile() {
        // do something
    }

}