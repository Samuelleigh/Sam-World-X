#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>
#include <stdint.h>


struct VirtActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename T1>
struct VirtActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
struct GenericVirtActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_virtual_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename T1>
struct GenericVirtActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj, T1 p1)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_virtual_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
struct InterfaceActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename T1>
struct InterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
struct GenericInterfaceActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_interface_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename T1>
struct GenericInterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (const RuntimeMethod* method, RuntimeObject* obj, T1 p1)
	{
		VirtualInvokeData invokeData;
		il2cpp_codegen_get_generic_interface_invoke_data(method, obj, &invokeData);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};

// Ink.Runtime.DebugMetadata
struct DebugMetadata_t103AC7BAE8229680610592AE0BCF0B9F9C110841;
// System.Delegate
struct Delegate_t;
// System.DelegateData
struct DelegateData_t17DD30660E330C49381DAA99F934BE75CB11F288;
// Ink.Runtime.Divert
struct Divert_tC6EC7216F73F1C59B966BAD665FE7EF23B7CB402;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// Ink.Parsed.Object
struct Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F;
// Ink.Runtime.Object
struct Object_tDC9B7528C2150F413509A6A7DA19A26BD4434B99;
// System.String
struct String_t;
// System.Void
struct Void_t700C6383A2A510C2CF4DD86DABD5CA9FF70ADAC5;
// Ink.Parsed.Weave/BadTerminationHandler
struct BadTerminationHandler_tF470B2AEC11A22E62496997221C44696EB40303E;
// Ink.Parsed.Weave/GatherPointToResolve
struct GatherPointToResolve_tD8449C5932C2D6FE48E68A3B200F8CB2957506AF;
// System.Collections.Generic.List`1<Ink.Parsed.Object>
struct List_1_t95E539FA27A8F8A490403D822B3DAA778D65EC5D;
// System.AsyncCallback
struct AsyncCallback_tA7921BEF974919C46FF8F9D9867C567B200BB0EA;
// System.Char[]
struct CharU5BU5D_t7B7FC5BC8091AA3B9CB0B29CDD80B5EE9254AA34;
// System.Delegate[]
struct DelegateU5BU5D_t677D8FE08A5F99E8EE49150B73966CD6E9BF7DB8;
// System.IAsyncResult
struct IAsyncResult_tC9F97BF36FCF122D29D3101D80642278297BF370;

struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;

struct DelegateU5BU5D_t677D8FE08A5F99E8EE49150B73966CD6E9BF7DB8;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Object


// Ink.Parsed.Object
struct  Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F  : public RuntimeObject
{
public:
	// Ink.Runtime.DebugMetadata Ink.Parsed.Object::_debugMetadata
	DebugMetadata_t103AC7BAE8229680610592AE0BCF0B9F9C110841 * ____debugMetadata_0;
	// Ink.Parsed.Object Ink.Parsed.Object::<parent>k__BackingField
	Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F * ___U3CparentU3Ek__BackingField_1;
	// System.Collections.Generic.List`1<Ink.Parsed.Object> Ink.Parsed.Object::<content>k__BackingField
	List_1_t95E539FA27A8F8A490403D822B3DAA778D65EC5D * ___U3CcontentU3Ek__BackingField_2;
	// Ink.Runtime.Object Ink.Parsed.Object::_runtimeObject
	Object_tDC9B7528C2150F413509A6A7DA19A26BD4434B99 * ____runtimeObject_3;
	// System.Boolean Ink.Parsed.Object::_alreadyHadError
	bool ____alreadyHadError_4;
	// System.Boolean Ink.Parsed.Object::_alreadyHadWarning
	bool ____alreadyHadWarning_5;

public:
	inline static int32_t get_offset_of__debugMetadata_0() { return static_cast<int32_t>(offsetof(Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F, ____debugMetadata_0)); }
	inline DebugMetadata_t103AC7BAE8229680610592AE0BCF0B9F9C110841 * get__debugMetadata_0() const { return ____debugMetadata_0; }
	inline DebugMetadata_t103AC7BAE8229680610592AE0BCF0B9F9C110841 ** get_address_of__debugMetadata_0() { return &____debugMetadata_0; }
	inline void set__debugMetadata_0(DebugMetadata_t103AC7BAE8229680610592AE0BCF0B9F9C110841 * value)
	{
		____debugMetadata_0 = value;
		Il2CppCodeGenWriteBarrier((void**)(&____debugMetadata_0), (void*)value);
	}

	inline static int32_t get_offset_of_U3CparentU3Ek__BackingField_1() { return static_cast<int32_t>(offsetof(Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F, ___U3CparentU3Ek__BackingField_1)); }
	inline Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F * get_U3CparentU3Ek__BackingField_1() const { return ___U3CparentU3Ek__BackingField_1; }
	inline Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F ** get_address_of_U3CparentU3Ek__BackingField_1() { return &___U3CparentU3Ek__BackingField_1; }
	inline void set_U3CparentU3Ek__BackingField_1(Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F * value)
	{
		___U3CparentU3Ek__BackingField_1 = value;
		Il2CppCodeGenWriteBarrier((void**)(&___U3CparentU3Ek__BackingField_1), (void*)value);
	}

	inline static int32_t get_offset_of_U3CcontentU3Ek__BackingField_2() { return static_cast<int32_t>(offsetof(Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F, ___U3CcontentU3Ek__BackingField_2)); }
	inline List_1_t95E539FA27A8F8A490403D822B3DAA778D65EC5D * get_U3CcontentU3Ek__BackingField_2() const { return ___U3CcontentU3Ek__BackingField_2; }
	inline List_1_t95E539FA27A8F8A490403D822B3DAA778D65EC5D ** get_address_of_U3CcontentU3Ek__BackingField_2() { return &___U3CcontentU3Ek__BackingField_2; }
	inline void set_U3CcontentU3Ek__BackingField_2(List_1_t95E539FA27A8F8A490403D822B3DAA778D65EC5D * value)
	{
		___U3CcontentU3Ek__BackingField_2 = value;
		Il2CppCodeGenWriteBarrier((void**)(&___U3CcontentU3Ek__BackingField_2), (void*)value);
	}

	inline static int32_t get_offset_of__runtimeObject_3() { return static_cast<int32_t>(offsetof(Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F, ____runtimeObject_3)); }
	inline Object_tDC9B7528C2150F413509A6A7DA19A26BD4434B99 * get__runtimeObject_3() const { return ____runtimeObject_3; }
	inline Object_tDC9B7528C2150F413509A6A7DA19A26BD4434B99 ** get_address_of__runtimeObject_3() { return &____runtimeObject_3; }
	inline void set__runtimeObject_3(Object_tDC9B7528C2150F413509A6A7DA19A26BD4434B99 * value)
	{
		____runtimeObject_3 = value;
		Il2CppCodeGenWriteBarrier((void**)(&____runtimeObject_3), (void*)value);
	}

	inline static int32_t get_offset_of__alreadyHadError_4() { return static_cast<int32_t>(offsetof(Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F, ____alreadyHadError_4)); }
	inline bool get__alreadyHadError_4() const { return ____alreadyHadError_4; }
	inline bool* get_address_of__alreadyHadError_4() { return &____alreadyHadError_4; }
	inline void set__alreadyHadError_4(bool value)
	{
		____alreadyHadError_4 = value;
	}

	inline static int32_t get_offset_of__alreadyHadWarning_5() { return static_cast<int32_t>(offsetof(Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F, ____alreadyHadWarning_5)); }
	inline bool get__alreadyHadWarning_5() const { return ____alreadyHadWarning_5; }
	inline bool* get_address_of__alreadyHadWarning_5() { return &____alreadyHadWarning_5; }
	inline void set__alreadyHadWarning_5(bool value)
	{
		____alreadyHadWarning_5 = value;
	}
};


// Ink.Parsed.Weave_GatherPointToResolve
struct  GatherPointToResolve_tD8449C5932C2D6FE48E68A3B200F8CB2957506AF  : public RuntimeObject
{
public:
	// Ink.Runtime.Divert Ink.Parsed.Weave_GatherPointToResolve::divert
	Divert_tC6EC7216F73F1C59B966BAD665FE7EF23B7CB402 * ___divert_0;
	// Ink.Runtime.Object Ink.Parsed.Weave_GatherPointToResolve::targetRuntimeObj
	Object_tDC9B7528C2150F413509A6A7DA19A26BD4434B99 * ___targetRuntimeObj_1;

public:
	inline static int32_t get_offset_of_divert_0() { return static_cast<int32_t>(offsetof(GatherPointToResolve_tD8449C5932C2D6FE48E68A3B200F8CB2957506AF, ___divert_0)); }
	inline Divert_tC6EC7216F73F1C59B966BAD665FE7EF23B7CB402 * get_divert_0() const { return ___divert_0; }
	inline Divert_tC6EC7216F73F1C59B966BAD665FE7EF23B7CB402 ** get_address_of_divert_0() { return &___divert_0; }
	inline void set_divert_0(Divert_tC6EC7216F73F1C59B966BAD665FE7EF23B7CB402 * value)
	{
		___divert_0 = value;
		Il2CppCodeGenWriteBarrier((void**)(&___divert_0), (void*)value);
	}

	inline static int32_t get_offset_of_targetRuntimeObj_1() { return static_cast<int32_t>(offsetof(GatherPointToResolve_tD8449C5932C2D6FE48E68A3B200F8CB2957506AF, ___targetRuntimeObj_1)); }
	inline Object_tDC9B7528C2150F413509A6A7DA19A26BD4434B99 * get_targetRuntimeObj_1() const { return ___targetRuntimeObj_1; }
	inline Object_tDC9B7528C2150F413509A6A7DA19A26BD4434B99 ** get_address_of_targetRuntimeObj_1() { return &___targetRuntimeObj_1; }
	inline void set_targetRuntimeObj_1(Object_tDC9B7528C2150F413509A6A7DA19A26BD4434B99 * value)
	{
		___targetRuntimeObj_1 = value;
		Il2CppCodeGenWriteBarrier((void**)(&___targetRuntimeObj_1), (void*)value);
	}
};

struct Il2CppArrayBounds;

// System.Array


// System.ValueType
struct  ValueType_tDBF999C1B75C48C68621878250DBF6CDBCF51E52  : public RuntimeObject
{
public:

public:
};

// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_tDBF999C1B75C48C68621878250DBF6CDBCF51E52_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_tDBF999C1B75C48C68621878250DBF6CDBCF51E52_marshaled_com
{
};

// System.Enum
struct  Enum_t23B90B40F60E677A8025267341651C94AE079CDA  : public ValueType_tDBF999C1B75C48C68621878250DBF6CDBCF51E52
{
public:

public:
};

struct Enum_t23B90B40F60E677A8025267341651C94AE079CDA_StaticFields
{
public:
	// System.Char[] System.Enum::enumSeperatorCharArray
	CharU5BU5D_t7B7FC5BC8091AA3B9CB0B29CDD80B5EE9254AA34* ___enumSeperatorCharArray_0;

public:
	inline static int32_t get_offset_of_enumSeperatorCharArray_0() { return static_cast<int32_t>(offsetof(Enum_t23B90B40F60E677A8025267341651C94AE079CDA_StaticFields, ___enumSeperatorCharArray_0)); }
	inline CharU5BU5D_t7B7FC5BC8091AA3B9CB0B29CDD80B5EE9254AA34* get_enumSeperatorCharArray_0() const { return ___enumSeperatorCharArray_0; }
	inline CharU5BU5D_t7B7FC5BC8091AA3B9CB0B29CDD80B5EE9254AA34** get_address_of_enumSeperatorCharArray_0() { return &___enumSeperatorCharArray_0; }
	inline void set_enumSeperatorCharArray_0(CharU5BU5D_t7B7FC5BC8091AA3B9CB0B29CDD80B5EE9254AA34* value)
	{
		___enumSeperatorCharArray_0 = value;
		Il2CppCodeGenWriteBarrier((void**)(&___enumSeperatorCharArray_0), (void*)value);
	}
};

// Native definition for P/Invoke marshalling of System.Enum
struct Enum_t23B90B40F60E677A8025267341651C94AE079CDA_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.Enum
struct Enum_t23B90B40F60E677A8025267341651C94AE079CDA_marshaled_com
{
};

// System.IntPtr
struct  IntPtr_t 
{
public:
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;

public:
	inline static int32_t get_offset_of_m_value_0() { return static_cast<int32_t>(offsetof(IntPtr_t, ___m_value_0)); }
	inline void* get_m_value_0() const { return ___m_value_0; }
	inline void** get_address_of_m_value_0() { return &___m_value_0; }
	inline void set_m_value_0(void* value)
	{
		___m_value_0 = value;
	}
};

struct IntPtr_t_StaticFields
{
public:
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;

public:
	inline static int32_t get_offset_of_Zero_1() { return static_cast<int32_t>(offsetof(IntPtr_t_StaticFields, ___Zero_1)); }
	inline intptr_t get_Zero_1() const { return ___Zero_1; }
	inline intptr_t* get_address_of_Zero_1() { return &___Zero_1; }
	inline void set_Zero_1(intptr_t value)
	{
		___Zero_1 = value;
	}
};


// System.Void
struct  Void_t700C6383A2A510C2CF4DD86DABD5CA9FF70ADAC5 
{
public:
	union
	{
		struct
		{
		};
		uint8_t Void_t700C6383A2A510C2CF4DD86DABD5CA9FF70ADAC5__padding[1];
	};

public:
};


// Ink.Runtime.SimpleJson_Writer_State
struct  State_t6F28AAC987FD8BBF89B8F7D483B972C1197567D4 
{
public:
	// System.Int32 Ink.Runtime.SimpleJson_Writer_State::value__
	int32_t ___value___2;

public:
	inline static int32_t get_offset_of_value___2() { return static_cast<int32_t>(offsetof(State_t6F28AAC987FD8BBF89B8F7D483B972C1197567D4, ___value___2)); }
	inline int32_t get_value___2() const { return ___value___2; }
	inline int32_t* get_address_of_value___2() { return &___value___2; }
	inline void set_value___2(int32_t value)
	{
		___value___2 = value;
	}
};


// System.Delegate
struct  Delegate_t  : public RuntimeObject
{
public:
	// System.IntPtr System.Delegate::method_ptr
	Il2CppMethodPointer ___method_ptr_0;
	// System.IntPtr System.Delegate::invoke_impl
	intptr_t ___invoke_impl_1;
	// System.Object System.Delegate::m_target
	RuntimeObject * ___m_target_2;
	// System.IntPtr System.Delegate::method
	intptr_t ___method_3;
	// System.IntPtr System.Delegate::delegate_trampoline
	intptr_t ___delegate_trampoline_4;
	// System.IntPtr System.Delegate::extra_arg
	intptr_t ___extra_arg_5;
	// System.IntPtr System.Delegate::method_code
	intptr_t ___method_code_6;
	// System.Reflection.MethodInfo System.Delegate::method_info
	MethodInfo_t * ___method_info_7;
	// System.Reflection.MethodInfo System.Delegate::original_method_info
	MethodInfo_t * ___original_method_info_8;
	// System.DelegateData System.Delegate::data
	DelegateData_t17DD30660E330C49381DAA99F934BE75CB11F288 * ___data_9;
	// System.Boolean System.Delegate::method_is_virtual
	bool ___method_is_virtual_10;

public:
	inline static int32_t get_offset_of_method_ptr_0() { return static_cast<int32_t>(offsetof(Delegate_t, ___method_ptr_0)); }
	inline Il2CppMethodPointer get_method_ptr_0() const { return ___method_ptr_0; }
	inline Il2CppMethodPointer* get_address_of_method_ptr_0() { return &___method_ptr_0; }
	inline void set_method_ptr_0(Il2CppMethodPointer value)
	{
		___method_ptr_0 = value;
	}

	inline static int32_t get_offset_of_invoke_impl_1() { return static_cast<int32_t>(offsetof(Delegate_t, ___invoke_impl_1)); }
	inline intptr_t get_invoke_impl_1() const { return ___invoke_impl_1; }
	inline intptr_t* get_address_of_invoke_impl_1() { return &___invoke_impl_1; }
	inline void set_invoke_impl_1(intptr_t value)
	{
		___invoke_impl_1 = value;
	}

	inline static int32_t get_offset_of_m_target_2() { return static_cast<int32_t>(offsetof(Delegate_t, ___m_target_2)); }
	inline RuntimeObject * get_m_target_2() const { return ___m_target_2; }
	inline RuntimeObject ** get_address_of_m_target_2() { return &___m_target_2; }
	inline void set_m_target_2(RuntimeObject * value)
	{
		___m_target_2 = value;
		Il2CppCodeGenWriteBarrier((void**)(&___m_target_2), (void*)value);
	}

	inline static int32_t get_offset_of_method_3() { return static_cast<int32_t>(offsetof(Delegate_t, ___method_3)); }
	inline intptr_t get_method_3() const { return ___method_3; }
	inline intptr_t* get_address_of_method_3() { return &___method_3; }
	inline void set_method_3(intptr_t value)
	{
		___method_3 = value;
	}

	inline static int32_t get_offset_of_delegate_trampoline_4() { return static_cast<int32_t>(offsetof(Delegate_t, ___delegate_trampoline_4)); }
	inline intptr_t get_delegate_trampoline_4() const { return ___delegate_trampoline_4; }
	inline intptr_t* get_address_of_delegate_trampoline_4() { return &___delegate_trampoline_4; }
	inline void set_delegate_trampoline_4(intptr_t value)
	{
		___delegate_trampoline_4 = value;
	}

	inline static int32_t get_offset_of_extra_arg_5() { return static_cast<int32_t>(offsetof(Delegate_t, ___extra_arg_5)); }
	inline intptr_t get_extra_arg_5() const { return ___extra_arg_5; }
	inline intptr_t* get_address_of_extra_arg_5() { return &___extra_arg_5; }
	inline void set_extra_arg_5(intptr_t value)
	{
		___extra_arg_5 = value;
	}

	inline static int32_t get_offset_of_method_code_6() { return static_cast<int32_t>(offsetof(Delegate_t, ___method_code_6)); }
	inline intptr_t get_method_code_6() const { return ___method_code_6; }
	inline intptr_t* get_address_of_method_code_6() { return &___method_code_6; }
	inline void set_method_code_6(intptr_t value)
	{
		___method_code_6 = value;
	}

	inline static int32_t get_offset_of_method_info_7() { return static_cast<int32_t>(offsetof(Delegate_t, ___method_info_7)); }
	inline MethodInfo_t * get_method_info_7() const { return ___method_info_7; }
	inline MethodInfo_t ** get_address_of_method_info_7() { return &___method_info_7; }
	inline void set_method_info_7(MethodInfo_t * value)
	{
		___method_info_7 = value;
		Il2CppCodeGenWriteBarrier((void**)(&___method_info_7), (void*)value);
	}

	inline static int32_t get_offset_of_original_method_info_8() { return static_cast<int32_t>(offsetof(Delegate_t, ___original_method_info_8)); }
	inline MethodInfo_t * get_original_method_info_8() const { return ___original_method_info_8; }
	inline MethodInfo_t ** get_address_of_original_method_info_8() { return &___original_method_info_8; }
	inline void set_original_method_info_8(MethodInfo_t * value)
	{
		___original_method_info_8 = value;
		Il2CppCodeGenWriteBarrier((void**)(&___original_method_info_8), (void*)value);
	}

	inline static int32_t get_offset_of_data_9() { return static_cast<int32_t>(offsetof(Delegate_t, ___data_9)); }
	inline DelegateData_t17DD30660E330C49381DAA99F934BE75CB11F288 * get_data_9() const { return ___data_9; }
	inline DelegateData_t17DD30660E330C49381DAA99F934BE75CB11F288 ** get_address_of_data_9() { return &___data_9; }
	inline void set_data_9(DelegateData_t17DD30660E330C49381DAA99F934BE75CB11F288 * value)
	{
		___data_9 = value;
		Il2CppCodeGenWriteBarrier((void**)(&___data_9), (void*)value);
	}

	inline static int32_t get_offset_of_method_is_virtual_10() { return static_cast<int32_t>(offsetof(Delegate_t, ___method_is_virtual_10)); }
	inline bool get_method_is_virtual_10() const { return ___method_is_virtual_10; }
	inline bool* get_address_of_method_is_virtual_10() { return &___method_is_virtual_10; }
	inline void set_method_is_virtual_10(bool value)
	{
		___method_is_virtual_10 = value;
	}
};

// Native definition for P/Invoke marshalling of System.Delegate
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	MethodInfo_t * ___method_info_7;
	MethodInfo_t * ___original_method_info_8;
	DelegateData_t17DD30660E330C49381DAA99F934BE75CB11F288 * ___data_9;
	int32_t ___method_is_virtual_10;
};
// Native definition for COM marshalling of System.Delegate
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	MethodInfo_t * ___method_info_7;
	MethodInfo_t * ___original_method_info_8;
	DelegateData_t17DD30660E330C49381DAA99F934BE75CB11F288 * ___data_9;
	int32_t ___method_is_virtual_10;
};

// Ink.Runtime.SimpleJson_Writer_StateElement
struct  StateElement_tF1B101B1016D3C0CDEE7C9CD85549AA281F406DA 
{
public:
	// Ink.Runtime.SimpleJson_Writer_State Ink.Runtime.SimpleJson_Writer_StateElement::type
	int32_t ___type_0;
	// System.Int32 Ink.Runtime.SimpleJson_Writer_StateElement::childCount
	int32_t ___childCount_1;

public:
	inline static int32_t get_offset_of_type_0() { return static_cast<int32_t>(offsetof(StateElement_tF1B101B1016D3C0CDEE7C9CD85549AA281F406DA, ___type_0)); }
	inline int32_t get_type_0() const { return ___type_0; }
	inline int32_t* get_address_of_type_0() { return &___type_0; }
	inline void set_type_0(int32_t value)
	{
		___type_0 = value;
	}

	inline static int32_t get_offset_of_childCount_1() { return static_cast<int32_t>(offsetof(StateElement_tF1B101B1016D3C0CDEE7C9CD85549AA281F406DA, ___childCount_1)); }
	inline int32_t get_childCount_1() const { return ___childCount_1; }
	inline int32_t* get_address_of_childCount_1() { return &___childCount_1; }
	inline void set_childCount_1(int32_t value)
	{
		___childCount_1 = value;
	}
};


// System.MulticastDelegate
struct  MulticastDelegate_t  : public Delegate_t
{
public:
	// System.Delegate[] System.MulticastDelegate::delegates
	DelegateU5BU5D_t677D8FE08A5F99E8EE49150B73966CD6E9BF7DB8* ___delegates_11;

public:
	inline static int32_t get_offset_of_delegates_11() { return static_cast<int32_t>(offsetof(MulticastDelegate_t, ___delegates_11)); }
	inline DelegateU5BU5D_t677D8FE08A5F99E8EE49150B73966CD6E9BF7DB8* get_delegates_11() const { return ___delegates_11; }
	inline DelegateU5BU5D_t677D8FE08A5F99E8EE49150B73966CD6E9BF7DB8** get_address_of_delegates_11() { return &___delegates_11; }
	inline void set_delegates_11(DelegateU5BU5D_t677D8FE08A5F99E8EE49150B73966CD6E9BF7DB8* value)
	{
		___delegates_11 = value;
		Il2CppCodeGenWriteBarrier((void**)(&___delegates_11), (void*)value);
	}
};

// Native definition for P/Invoke marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates_11;
};
// Native definition for COM marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates_11;
};

// Ink.Parsed.Weave_BadTerminationHandler
struct  BadTerminationHandler_tF470B2AEC11A22E62496997221C44696EB40303E  : public MulticastDelegate_t
{
public:

public:
};


// System.AsyncCallback
struct  AsyncCallback_tA7921BEF974919C46FF8F9D9867C567B200BB0EA  : public MulticastDelegate_t
{
public:

public:
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
// System.Delegate[]
struct DelegateU5BU5D_t677D8FE08A5F99E8EE49150B73966CD6E9BF7DB8  : public RuntimeArray
{
public:
	ALIGN_FIELD (8) Delegate_t * m_Items[1];

public:
	inline Delegate_t * GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Delegate_t ** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Delegate_t * value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Delegate_t * GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Delegate_t ** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Delegate_t * value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};



// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_m88880E0413421D13FD95325EDCE231707CE1F405 (RuntimeObject * __this, const RuntimeMethod* method);
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void Ink.Parsed.Weave_BadTerminationHandler::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BadTerminationHandler__ctor_mC841CC7F4459319BF59C74FEADB5E4B0942249AE (BadTerminationHandler_tF470B2AEC11A22E62496997221C44696EB40303E * __this, RuntimeObject * ___object0, intptr_t ___method1, const RuntimeMethod* method)
{
	__this->set_method_ptr_0(il2cpp_codegen_get_method_pointer((RuntimeMethod*)___method1));
	__this->set_method_3(___method1);
	__this->set_m_target_2(___object0);
}
// System.Void Ink.Parsed.Weave_BadTerminationHandler::Invoke(Ink.Parsed.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BadTerminationHandler_Invoke_m014D2749E002B8CB08C2180BFFDB4F85259EE2A7 (BadTerminationHandler_tF470B2AEC11A22E62496997221C44696EB40303E * __this, Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F * ___terminatingObj0, const RuntimeMethod* method)
{
	DelegateU5BU5D_t677D8FE08A5F99E8EE49150B73966CD6E9BF7DB8* delegateArrayToInvoke = __this->get_delegates_11();
	Delegate_t** delegatesToInvoke;
	il2cpp_array_size_t length;
	if (delegateArrayToInvoke != NULL)
	{
		length = delegateArrayToInvoke->max_length;
		delegatesToInvoke = reinterpret_cast<Delegate_t**>(delegateArrayToInvoke->GetAddressAtUnchecked(0));
	}
	else
	{
		length = 1;
		delegatesToInvoke = reinterpret_cast<Delegate_t**>(&__this);
	}

	for (il2cpp_array_size_t i = 0; i < length; i++)
	{
		Delegate_t* currentDelegate = delegatesToInvoke[i];
		Il2CppMethodPointer targetMethodPointer = currentDelegate->get_method_ptr_0();
		RuntimeObject* targetThis = currentDelegate->get_m_target_2();
		RuntimeMethod* targetMethod = (RuntimeMethod*)(currentDelegate->get_method_3());
		if (!il2cpp_codegen_method_is_virtual(targetMethod))
		{
			il2cpp_codegen_raise_execution_engine_exception_if_method_is_not_found(targetMethod);
		}
		bool ___methodIsStatic = MethodIsStatic(targetMethod);
		int ___parameterCount = il2cpp_codegen_method_parameter_count(targetMethod);
		if (___methodIsStatic)
		{
			if (___parameterCount == 1)
			{
				// open
				typedef void (*FunctionPointerType) (Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F *, const RuntimeMethod*);
				((FunctionPointerType)targetMethodPointer)(___terminatingObj0, targetMethod);
			}
			else
			{
				// closed
				typedef void (*FunctionPointerType) (void*, Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F *, const RuntimeMethod*);
				((FunctionPointerType)targetMethodPointer)(targetThis, ___terminatingObj0, targetMethod);
			}
		}
		else if (___parameterCount != 1)
		{
			// open
			if (il2cpp_codegen_method_is_virtual(targetMethod) && il2cpp_codegen_delegate_has_invoker((Il2CppDelegate*)__this))
			{
				if (il2cpp_codegen_method_is_generic_instance(targetMethod))
				{
					if (il2cpp_codegen_method_is_interface_method(targetMethod))
						GenericInterfaceActionInvoker0::Invoke(targetMethod, ___terminatingObj0);
					else
						GenericVirtActionInvoker0::Invoke(targetMethod, ___terminatingObj0);
				}
				else
				{
					if (il2cpp_codegen_method_is_interface_method(targetMethod))
						InterfaceActionInvoker0::Invoke(il2cpp_codegen_method_get_slot(targetMethod), il2cpp_codegen_method_get_declaring_type(targetMethod), ___terminatingObj0);
					else
						VirtActionInvoker0::Invoke(il2cpp_codegen_method_get_slot(targetMethod), ___terminatingObj0);
				}
			}
			else
			{
				typedef void (*FunctionPointerType) (Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F *, const RuntimeMethod*);
				((FunctionPointerType)targetMethodPointer)(___terminatingObj0, targetMethod);
			}
		}
		else
		{
			// closed
			if (targetThis != NULL && il2cpp_codegen_method_is_virtual(targetMethod) && !il2cpp_codegen_object_is_of_sealed_type(targetThis) && il2cpp_codegen_delegate_has_invoker((Il2CppDelegate*)__this))
			{
				if (il2cpp_codegen_method_is_generic_instance(targetMethod))
				{
					if (il2cpp_codegen_method_is_interface_method(targetMethod))
						GenericInterfaceActionInvoker1< Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F * >::Invoke(targetMethod, targetThis, ___terminatingObj0);
					else
						GenericVirtActionInvoker1< Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F * >::Invoke(targetMethod, targetThis, ___terminatingObj0);
				}
				else
				{
					if (il2cpp_codegen_method_is_interface_method(targetMethod))
						InterfaceActionInvoker1< Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F * >::Invoke(il2cpp_codegen_method_get_slot(targetMethod), il2cpp_codegen_method_get_declaring_type(targetMethod), targetThis, ___terminatingObj0);
					else
						VirtActionInvoker1< Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F * >::Invoke(il2cpp_codegen_method_get_slot(targetMethod), targetThis, ___terminatingObj0);
				}
			}
			else
			{
				if (targetThis == NULL && il2cpp_codegen_class_is_value_type(il2cpp_codegen_method_get_declaring_type(targetMethod)))
				{
					typedef void (*FunctionPointerType) (RuntimeObject*, const RuntimeMethod*);
					((FunctionPointerType)targetMethodPointer)((reinterpret_cast<RuntimeObject*>(___terminatingObj0) - 1), targetMethod);
				}
				if (targetThis == NULL)
				{
					typedef void (*FunctionPointerType) (Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F *, const RuntimeMethod*);
					((FunctionPointerType)targetMethodPointer)(___terminatingObj0, targetMethod);
				}
				else
				{
					typedef void (*FunctionPointerType) (void*, Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F *, const RuntimeMethod*);
					((FunctionPointerType)targetMethodPointer)(targetThis, ___terminatingObj0, targetMethod);
				}
			}
		}
	}
}
// System.IAsyncResult Ink.Parsed.Weave_BadTerminationHandler::BeginInvoke(Ink.Parsed.Object,System.AsyncCallback,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* BadTerminationHandler_BeginInvoke_mEDAC8D02CF13A4509AFA439D08BAEFAB4C860AA8 (BadTerminationHandler_tF470B2AEC11A22E62496997221C44696EB40303E * __this, Object_tDBE950EAAE7ABF95A67473C71A199FA9324C326F * ___terminatingObj0, AsyncCallback_tA7921BEF974919C46FF8F9D9867C567B200BB0EA * ___callback1, RuntimeObject * ___object2, const RuntimeMethod* method)
{
	void *__d_args[2] = {0};
	__d_args[0] = ___terminatingObj0;
	return (RuntimeObject*)il2cpp_codegen_delegate_begin_invoke((RuntimeDelegate*)__this, __d_args, (RuntimeDelegate*)___callback1, (RuntimeObject*)___object2);
}
// System.Void Ink.Parsed.Weave_BadTerminationHandler::EndInvoke(System.IAsyncResult)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BadTerminationHandler_EndInvoke_mD4AFA70290E414108EA93C5DB593303D7619501F (BadTerminationHandler_tF470B2AEC11A22E62496997221C44696EB40303E * __this, RuntimeObject* ___result0, const RuntimeMethod* method)
{
	il2cpp_codegen_delegate_end_invoke((Il2CppAsyncResult*) ___result0, 0);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void Ink.Parsed.Weave_GatherPointToResolve::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GatherPointToResolve__ctor_mDBD4602A8A5556A5C740B3318A180637491A691F (GatherPointToResolve_tD8449C5932C2D6FE48E68A3B200F8CB2957506AF * __this, const RuntimeMethod* method)
{
	{
		Object__ctor_m88880E0413421D13FD95325EDCE231707CE1F405(__this, /*hidden argument*/NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
