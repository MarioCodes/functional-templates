package es.codes.mario.template.fragment

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.databinding.DataBindingUtil
import androidx.databinding.ViewDataBinding
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProviders
import androidx.navigation.fragment.findNavController
import es.codes.mario.template.R
import es.codes.mario.template.charts.AndroidPlotChart
import es.codes.mario.template.di.DaggerAppComponent
import es.codes.mario.template.viewmodel.EntryViewModel
import kotlinx.android.synthetic.main.graph_example.view.*
import javax.inject.Inject


/**
 * A simple [Fragment] subclass as the default destination in the navigation.
 */
class MainFragment : Fragment() {

    private lateinit var binding: ViewDataBinding

    @Inject
    lateinit var chart: AndroidPlotChart

    private lateinit var entryViewModel: EntryViewModel

    init {
        DaggerAppComponent.create().inject(this)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        binding = DataBindingUtil.inflate<ViewDataBinding>(inflater, R.layout.graph_example, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.root.button_add_entry.setOnClickListener {
            findNavController().navigate(R.id.action_FirstFragment_to_addEntryFragment)
        }

        binding.root.button_delete_entry_entry.setOnClickListener {
            findNavController().navigate(R.id.action_FirstFragment_to_deleteEntryFragment)
        }

        entryViewModel = ViewModelProviders.of(requireActivity()).get(EntryViewModel::class.java)
        observeViewModel()
    }

    private fun observeViewModel() {
        // first we set the observers to react to changes, and then force the first refresh.
        entryViewModel.entries.observe(viewLifecycleOwner, Observer { entries ->
            // if here is because the value changed. react.
        })
    }

}