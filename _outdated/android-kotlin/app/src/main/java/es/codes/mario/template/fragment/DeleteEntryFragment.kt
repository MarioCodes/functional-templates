package es.codes.mario.template.fragment

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.databinding.DataBindingUtil
import androidx.databinding.ViewDataBinding
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProviders
import androidx.navigation.fragment.findNavController
import es.codes.mario.template.R
import es.codes.mario.template.di.DaggerAppComponent
import es.codes.mario.template.viewmodel.EntryViewModel
import kotlinx.android.synthetic.main.list_delete_entry_example.view.*

class DeleteEntryFragment : Fragment() {

    private lateinit var binding: ViewDataBinding

    private lateinit var entryViewModel: EntryViewModel

    init {
        DaggerAppComponent.create().inject(this)
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        binding = DataBindingUtil.inflate<ViewDataBinding>(
            inflater,
            R.layout.list_delete_entry_example,
            container,
            false
        )
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.root.button_delete_entry_cancel.setOnClickListener {
            findNavController().navigate(R.id.action_deleteEntryFragment_to_FirstFragment)
        }

        entryViewModel = ViewModelProviders.of(requireActivity()).get(EntryViewModel::class.java)

        // set n entries here
        val linearLayout = binding.root.linear_layout_entries_container
        // this removes placeholder view.
        linearLayout.removeAllViews()

        entryViewModel.entries.value?.let { entries ->
            // do something here
        }
    }

}